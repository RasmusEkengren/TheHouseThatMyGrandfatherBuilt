using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnInput : MonoBehaviour
{
    [SerializeField] private Button buttonToSelect = null;
    private bool anthingSelected = false;

    public void SelectButton()
    {
        if (!EventSystem.current.alreadySelecting)
        {
            buttonToSelect.Select();
        }
    }
}
