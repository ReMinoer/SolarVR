using UnityEngine;
using System.Collections;

[RequireComponent(typeof(VRActor))]
[RequireComponent(typeof(AudioSource))]

public class InterctionalSound : MonoBehaviour {

    private AudioSource source;
    public AudioClip clip;

    void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        source.clip = clip;
    }

    protected void OnMVRWandButtonPressed(VRSelection iSelection)
    {
        source.Play();
    }
}
