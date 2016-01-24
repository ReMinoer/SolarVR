using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NavigationKeyPoint : MonoBehaviour
{
    public List<NavigationKeyPoint> adjacentsKeyPoints;
    public string pointName;

    public void Details(Text displayer)
    {
        displayer.text = pointName;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(0.5f,0.5f,0.5f));
    }
}
