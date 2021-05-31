using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FirstSelection : MonoBehaviour
{
    [SerializeField] private Button buttonToSelect = null;

    private void Update()
    {
    }

    public void Selecting(InputAction.CallbackContext input)
    {
        //if (input.performed == true)
        //{

        //}
    }
}
