using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCuttingSounds : MonoBehaviour
{
    [SerializeField] [FMODUnity.EventRef] private string hitSound = null;
    [SerializeField] [FMODUnity.EventRef] private string missSound = null;

    public void PlayHitSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(hitSound);
    }

    public void PlayMissSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(missSound);
    }
}
