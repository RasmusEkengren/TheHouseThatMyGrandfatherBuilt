using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    [FMODUnity.EventRef] public string hoverSound;
    [FMODUnity.EventRef] public string pressedSound;

    public void PlayPressedSound()
    {
        if (pressedSound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(pressedSound);
        }
    }

    public void PlaySelectedSound()
    {
        if (hoverSound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(hoverSound);
        }
    }
}
