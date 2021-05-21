using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeahSounds : MonoBehaviour
{
    [FMODUnity.EventRef] [SerializeField] private string happy = null;
    [FMODUnity.EventRef] [SerializeField] private string sad = null;
    [FMODUnity.EventRef] [SerializeField] private string idle = null;

    public void LeahHappySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(happy);
    }
    public void LeahSadSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(sad);
    }
    public void LeahIdleSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(idle);
    }

}
