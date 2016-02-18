using UnityEngine;
using System.Collections.Generic;

public class AutoNavigationManager : MonoBehaviour
{
    private string _pathName;
    public List<NavigationKeyPoint> keyPoints;

    public bool Enabled { get; private set; }
    public bool IsMoving { get; private set; }
    public NavigationKeyPoint CurrentKeyPoint { get; private set; }
    public NavigationKeyPoint TargetedKeyPoint { get; private set; }
    public List<NavigationKeyPoint> AdjacentsKeyPoints { get; private set; }

    void Start()
    {
        CurrentKeyPoint = keyPoints[0];
        AdjacentsKeyPoints = CurrentKeyPoint.adjacentsKeyPoints;
    }

    void Update()
    {
        if (IsMoving)
            return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Enabled = !Enabled;
            if (Enabled)
                ActivateAutoNavigation();
            else
                ActivateManualNavigation();
        }

        if (Enabled)
        {
            TargetedKeyPoint = ClosestKeypoint();
            if (TargetedKeyPoint != null && MiddleVR.VRDeviceMgr.IsWandButtonToggled(0, true))
                ChoosePath();
        }
    }

    private void ActivateManualNavigation()
    {
        iTween.Stop(gameObject);
    }

    private void ActivateAutoNavigation()
    {
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

        _pathName = startPositionName + '-' + TargetedKeyPoint.pointName;
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(_pathName), "easetype", iTween.EaseType.linear, "speed", 2.0, "onstart", "StartWalking", "oncomplete", "StopWalking"));

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
        IsMoving = true;
    }

    void StopWalking()
    {
        IsMoving = false;
        if (AdjacentsKeyPoints.Count == 1)
        {
            TargetedKeyPoint = ClosestKeypoint();
            ChoosePath();
        }
    }
}