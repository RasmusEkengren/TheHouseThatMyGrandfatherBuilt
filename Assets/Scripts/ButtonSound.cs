using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    [FMODUnity.EventRef] public string hoverSound;
    [FMODUnity.EventRef] public string clickSound;

    public void PlayHoverSound()
    {
        if (hoverSound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(hoverSound);
        }
    }
    public void PlayClickSound()
    {
        if (clickSound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(clickSound);
        }
    }
}
