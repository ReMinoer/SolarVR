using UnityEngine;
using System.Collections;

[RequireComponent(typeof(VRActor))]
[RequireComponent(typeof(Animator))]
public class DoorBehaviour : MonoBehaviour
{
    private bool _isOpen = false;

    protected void OnMVRWandEnter(VRSelection iSelection)
    {
        GetComponentInChildren<Renderer>().material.color = Color.green;
    }

    protected void OnMVRWandExit(VRSelection iSelection)
    {
        GetComponentInChildren<Renderer>().material.color = Color.white;
    }

    protected void OnMVRWandButtonPressed(VRSelection iSelection)
    {
        var animator = GetComponent<Animator>();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
            return;

        _isOpen = !_isOpen;
        animator.SetBool("IsOpen", _isOpen);
    }
}
