using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class InteractionalSound : MonoBehaviour {

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

    public AudioClip SwitchClip(AudioClip newClip)
    {
        AudioClip temp = clip;
        clip = newClip;
        return temp;
    }
}
