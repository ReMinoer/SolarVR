using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NavigationKeyPoint : MonoBehaviour
{
    public List<NavigationKeyPoint> adjacentsKeyPoints;
    public string pointName;
    public Text detailDisplayer;

    public void Details()
    {
        detailDisplayer.text = pointName;
        Debug.Log(pointName);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(0.5f,0.5f,0.5f));
    }
}
