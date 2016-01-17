using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

[RequireComponent(typeof(FirstPersonController))]

public class AutoNavigationManager : MonoBehaviour {

    private FirstPersonController manualNavigation;

    private string pathName;
    public List<NavigationKeyPoint> keyPoints;
    private NavigationKeyPoint closerKeyPoint;
    private List<NavigationKeyPoint> adjacentsKeyPoints;

    private bool autoEnable = false;
    private bool isWalking = false;

    void Awake()
    {
        manualNavigation = GetComponent<FirstPersonController>();
    }

    void Start()
    {
        closerKeyPoint = keyPoints[0];
        adjacentsKeyPoints = closerKeyPoint.adjacentsKeyPoints;
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
           autoEnable = !autoEnable;
           if(autoEnable)
            {
                ActivateAutoNavigation();
            }
            else
            {
                ActivateManualNavigation();
            }
        }
        if (Input.GetKeyDown("space"))
        {
            if(autoEnable)
            {
                ChoosePath();
            }
        }
        if(autoEnable && !isWalking)
        {
            NavigationKeyPoint nextKeyPoint = ClosestKeypoint();
            nextKeyPoint.Details();
        }
    }

    private void ActivateManualNavigation()
    {
        iTween.Stop(gameObject);
    }

    private void ActivateAutoNavigation()
    {
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
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "speed", 1.5));

        closerKeyPoint = nextKeyPoint;
        adjacentsKeyPoints = closerKeyPoint.adjacentsKeyPoints;
    }

    private NavigationKeyPoint ClosestKeypoint()
    {
        Vector3 pathDirection = transform.position + transform.forward * 15.0f; // Pas sûr du tout
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
}