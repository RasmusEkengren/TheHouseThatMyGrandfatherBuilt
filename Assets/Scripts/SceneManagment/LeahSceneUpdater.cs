using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeahSceneUpdater : MonoBehaviour
{

    /// <summary>
    ///  Check globals
    ///  Depending on globals, do events
    ///  Thats basically it
    /// </summary>
    /// 

    public GameObject BrokenPorch = null;
    public GameObject FlatPorch = null;
    public GameObject SlantedPorch = null;
    public GameObject BrokenWindows = null;
    public GameObject BrokenWindowsEvent = null;
    public GameObject FixedWindows = null;
    public GameObject PorchEvent = null;

    private void Start()
    {

        Debug.Log("STartu " + GlobalSceneData.mg_porchFixed);
        if (GlobalSceneData.mg_porchFixed == true)
        {
            BrokenWindowsEvent.SetActive(true);
            PorchEvent.SetActive(false);

            if (GlobalSceneData.porchFixed == GlobalSceneData.PorchFixed.Flat)
            {
                FlatPorch.SetActive(true);
                BrokenPorch.SetActive(false);
                Debug.Log("Porch is fixed: " + FlatPorch.name, FlatPorch);
            }

            if (GlobalSceneData.porchFixed == GlobalSceneData.PorchFixed.Slanted)
            {
                SlantedPorch.SetActive(true);
                BrokenPorch.SetActive(false);
                Debug.Log("Porch is fixed: " + SlantedPorch.name, SlantedPorch);
            }
        }
        else if (GlobalSceneData.mg_porchFixed == false)
        {
            PorchEvent.SetActive(true);
            BrokenWindowsEvent.SetActive(false);
            BrokenPorch.SetActive(true);
            FlatPorch.SetActive(false);
            SlantedPorch.SetActive(false);
            FixedWindows.SetActive(false);

            Debug.Log("Porch still broken inside :(");
        }

        if (GlobalSceneData.mg_windowsFixed)
        {
            BrokenWindowsEvent.SetActive(false);
            BrokenWindows.SetActive(false);
            FixedWindows.SetActive(true);
            Debug.Log("Windows fixed");
        }
    }
}
