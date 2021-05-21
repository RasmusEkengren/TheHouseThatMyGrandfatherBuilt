using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeSounds : MonoBehaviour
{
    [FMODUnity.EventRef] [SerializeField] private string proud = null;
    [FMODUnity.EventRef] [SerializeField] private string random = null;

    public void GeorgeProudSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(proud);
    }
    public void GeorgeRandomSound()
    {

        FMODUnity.RuntimeManager.PlayOneShot(random);
    }
}
