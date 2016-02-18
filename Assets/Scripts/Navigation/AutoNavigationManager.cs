using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AutoNavigationManager : MonoBehaviour
{
    private string pathName;
    private bool autoEnable = false;
    private bool isWalking = false;

    public List<NavigationKeyPoint> keyPoints;
    public Text displayer;

    public NavigationKeyPoint CurrentKeyPoint { get; private set; }
    public NavigationKeyPoint TargetedKeyPoint { get; private set; }
    public List<NavigationKeyPoint> AdjacentsKeyPoints { get; private set; }

    void Start()
    {
        CurrentKeyPoint = keyPoints[0];
        AdjacentsKeyPoints = CurrentKeyPoint.adjacentsKeyPoints;
        displayer.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isWalking)
            return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            autoEnable = !autoEnable;
            if (autoEnable)
                ActivateAutoNavigation();
            else
                ActivateManualNavigation();
        }

        if (autoEnable)
        {
            TargetedKeyPoint = ClosestKeypoint();
            if (TargetedKeyPoint != null)
            {
                TargetedKeyPoint.Details(displayer);

                if (MiddleVR.VRDeviceMgr.IsWandButtonToggled(0, true))
                    ChoosePath();
            }
        }
    }

    private void ActivateManualNavigation()
    {
        displayer.gameObject.SetActive(false);
        iTween.Stop(gameObject);
    }

    private void ActivateAutoNavigation()
    {
        displayer.gameObject.SetActive(true);
        Vector3 position = transform.position;
        float distance = Vector3.Distance(CurrentKeyPoint.transform.position, position);
        
        for (int i = 0; i < keyPoints.Count; i++)
        {
            float newDistance = Vector3.Distance(keyPoints[i].transform.position, position);
            if (newDistance < distance)
            {
                distance = newDistance;
                CurrentKeyPoint = keyPoints[i];
            }
        }
        AdjacentsKeyPoints = CurrentKeyPoint.adjacentsKeyPoints;
        BlinkToCloserKeyPoint();
    }

    private void BlinkToCloserKeyPoint()
    {
        transform.position = CurrentKeyPoint.transform.position;
    }

    private void ChoosePath()
    {
        string startPositionName = CurrentKeyPoint.pointName;

        pathName = startPositionName + '-' + TargetedKeyPoint.pointName;
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "easetype", iTween.EaseType.linear, "speed", 2.0, "onstart", "StartWalking", "oncomplete", "StopWalking"));

        CurrentKeyPoint = TargetedKeyPoint;
        AdjacentsKeyPoints = CurrentKeyPoint.adjacentsKeyPoints;
    }

    private NavigationKeyPoint ClosestKeypoint()
    {
        float dotMax = float.MinValue;
        NavigationKeyPoint result = null;

        foreach (NavigationKeyPoint keyPoint in AdjacentsKeyPoints)
        {
            float dot = Vector3.Dot(transform.forward.normalized, (keyPoint.transform.position - transform.position).normalized);
            if (dot > dotMax)
            {
                result = keyPoint;
                dotMax = dot;
            }
        }
        return result;
    }

    void StartWalking()
    {
        displayer.gameObject.SetActive(false);
        isWalking = true;
    }

    void StopWalking()
    {
        isWalking = false;
        displayer.gameObject.SetActive(true);
        if (AdjacentsKeyPoints.Count == 1)
        {
            TargetedKeyPoint = ClosestKeypoint();
            ChoosePath();
        }
    }
}