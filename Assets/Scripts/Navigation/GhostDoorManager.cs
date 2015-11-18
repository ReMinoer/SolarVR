using System.Linq;
using UnityEngine;

/* Transparent materials :
 * 
 * - Bois grain pin alpha_1_1_masked.mat
 * - GDLM152_kaso Aluminium.mat
 * - kaso_dark_oak_1_1_masked.mat
 */

public class GhostDoorManager : MonoBehaviour
{
    public GameObject DoorsRootNode;
    public GameObject[] Ignored;

    void Awake()
    {
        foreach (Transform child in DoorsRootNode.transform)
        {
            GameObject door = child.gameObject;
            if (Ignored.Contains(door))
                continue;

            door.AddComponent<GhostDoor>();
            foreach (MeshCollider meshCollider in door.GetComponentsInChildren<MeshCollider>())
                meshCollider.enabled = false;
        }
    }
}
