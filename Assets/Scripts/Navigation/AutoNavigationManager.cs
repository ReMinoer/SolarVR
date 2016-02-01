using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AutoNavigationManager : MonoBehaviour {

    private string pathName;
    public List<NavigationKeyPoint> keyPoints;
    private NavigationKeyPoint closerKeyPoint;
    private List<NavigationKeyPoint> adjacentsKeyPoints;

    private bool autoEnable = false;
    private bool isWalking = false;

    public Text displayer;
    void Start()
    {
        closerKeyPoint = keyPoints[0];
        adjacentsKeyPoints = closerKeyPoint.adjacentsKeyPoints;
        displayer.gameObject.SetActive(false);
    }

    void Update()
    {
        if(!isWalking)
        {
            if (MiddleVR.VRDeviceMgr.IsWandButtonPressed(1))
            {
                autoEnable = !autoEnable;
                if (autoEnable)
                {
                    ActivateAutoNavigation();
                }
                else
                {
                    ActivateManualNavigation();
                }
            }
            if (MiddleVR.VRDeviceMgr.IsWandButtonPressed(2))
            {
                if (autoEnable)
                {
                    ChoosePath();
                }
            }
            if (autoEnable)
            {
               NavigationKeyPoint nextKeyPoint = ClosestKeypoint();
                nextKeyPoint.Details(displayer);
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
        float distance = Vector3.Distance(closerKeyPoint.transform.position, position);
        
        for (int i = 0; i < keyPoints.Count; i++)
        {
            float newDistance = Vector3.Distance(keyPoints[i].transform.position, position);
            if (newDistance < distance)
            {
                distance = newDistance;
                closerKeyPoint = keyPoints[i];
            }
        }
        adjacentsKeyPoints = closerKeyPoint.adjacentsKeyPoints;
        BlinkToCloserKeyPoint();
    }

    private void BlinkToCloserKeyPoint()
    {
        transform.position = closerKeyPoint.transform.position;
    }

    private void ChoosePath()
    {
        NavigationKeyPoint nextKeyPoint = ClosestKeypoint();
        string StartPositionName = closerKeyPoint.pointName;
        
        pathName = StartPositionName + '-' + nextKeyPoint.pointName;
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName),"easetype", iTween.EaseType.linear, "speed", 2.0, "onstart", "StartWalking","oncomplete","StopWalking"));

        closerKeyPoint = nextKeyPoint;
        adjacentsKeyPoints = closerKeyPoint.adjacentsKeyPoints;
    }

    private NavigationKeyPoint ClosestKeypoint()
    {
        Vector3 pathDirection = transform.position + transform.forward * 15.0f;
        float distance = Vector3.Distance(pathDirection, adjacentsKeyPoints[0].transform.position);

        NavigationKeyPoint nextKeyPoint = adjacentsKeyPoints[0];

        for (int i = 0; i < adjacentsKeyPoints.Count; i++)
        {
            float newDistance = Vector3.Distance(pathDirection, adjacentsKeyPoints[i].transform.position);
            if (newDistance < distance)
            {
                nextKeyPoint = adjacentsKeyPoints[i];
                distance = newDistance;
            }
        }
        return nextKeyPoint;
    }

    void StopWalking()
    {
        isWalking = false;
        displayer.gameObject.SetActive(true);
        if (adjacentsKeyPoints.Count == 1)
        {
            ChoosePath();
        }
    }

    void StartWalking()
    {
        displayer.gameObject.SetActive(false);
        isWalking = true;
    }
}