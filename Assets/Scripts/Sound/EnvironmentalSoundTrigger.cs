using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EnvironmentalSoundTrigger : MonoBehaviour
{
    public AudioClip clip;
    // Note: Ajouter variable pour différencier les salles

    void OnTriggerEnter(Collider col)
    {
        EnvironmentalSound player = col.gameObject.GetComponent<EnvironmentalSound>();
        if (player != null)
        {
            player.PlaySound(this);
        }
    }

}
