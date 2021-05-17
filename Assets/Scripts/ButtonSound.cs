using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    [FMODUnity.EventRef] public string hoverSound;
    [FMODUnity.EventRef] public string acceptSound;
    [FMODUnity.EventRef] public string denySound;

    public void PlayHoverSound()
    {
        if (hoverSound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(hoverSound);
        }
    }
    public void PlayAcceptSound()
    {
        if (acceptSound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(acceptSound);
        }
    }
    public void PlayDenySound()
    {
        if (denySound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(denySound);
        }
    }
}
