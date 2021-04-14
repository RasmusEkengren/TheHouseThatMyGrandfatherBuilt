using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    private Button button = null;

    [Space]
    [FMODUnity.EventRef] public string hoverSound;
    [FMODUnity.EventRef] public string clickSound;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(hoverSound);
        }    
    }

    public void OnMouseEnter()
    {
    }


    public void OnMouseDown()
    {
    }

    public void OnPoinerClick(PointerEventData eventData)
    {
        if (clickSound.Length > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(clickSound);
        }       
    }
}
