using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class AutoNavigationManager : MonoBehaviour {

    public List<GameObject> targets;
    private Transform currentTarget;
    private int targetIndice;

    private bool targetReach;
    private NavMeshAgent agent;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start ()
    {
        targetIndice = 0;
        targetReach = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (targetReach)
            OnNewTarget();
            
        float distance = Vector3.Distance(transform.position, currentTarget.position);
        if (distance < 2f)
            OnTargetReached();
    }

    void OnTargetReached()
    {
        Debug.Log("TargetReach");
        agent.Stop();
        targetReach = true;
    }

    void OnNewTarget()
    {
        Debug.Log("NewTarget");
        if (targetIndice >= targets.Count-1)
            targetIndice = 0;
        else
            targetIndice++;
        Debug.Log(targetIndice);

        currentTarget = targets[targetIndice].transform;
        agent.SetDestination(currentTarget.position);
        agent.Resume();

        targetReach = false;
        agent.gameObject.SetActive(true);
    }
}
