using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeSceneUpdater : MonoBehaviour
{
    public GameObject Axe;
    public GameObject ScrewMinigame;
    public GameObject Tree;
    public GameObject FlatPorch;
    public GameObject SlantedPorch;
    public GameObject PorchEvent;

    private void Start()
    {
        Debug.Log("Staaarrt");
        if (GlobalSceneData.georgeState == GlobalSceneData.GeorgeState.Porch)
        {
            Debug.Log("Porch");

            PorchEvent.SetActive(true);
            ScrewMinigame.SetActive(false);
            Tree.SetActive(true);
            Axe.SetActive(true);
        }

        if (GlobalSceneData.georgeState == GlobalSceneData.GeorgeState.Windows)
        {
            Debug.Log("Windows");
            ScrewMinigame.SetActive(true);
            Tree.SetActive(false);
            Axe.SetActive(false);

            if (GlobalSceneData.porchFixed == GlobalSceneData.PorchFixed.Flat)
            {
                FlatPorch.SetActive(true);
                PorchEvent.SetActive(false);
            }
            if (GlobalSceneData.porchFixed == GlobalSceneData.PorchFixed.Slanted)
            {
                SlantedPorch.SetActive(true);
                PorchEvent.SetActive(false);
            }

        }
    }
}
