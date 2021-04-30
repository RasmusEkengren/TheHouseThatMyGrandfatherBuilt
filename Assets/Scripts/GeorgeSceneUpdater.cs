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
	public GameObject StartThoughtEvent;
	public GameObject StartScrewEvent;

	private void Start()
	{
		Debug.Log("Staaarrt");
		if (GlobalSceneData.georgeState == GlobalSceneData.GeorgeState.Porch)
		{
			Debug.Log("Porch");

			StartThoughtEvent.SetActive(true);
			StartScrewEvent.SetActive(false);
			PorchEvent.SetActive(true);
			ScrewMinigame.SetActive(false);
			Tree.SetActive(true);
			Axe.SetActive(true);
		}

		if (GlobalSceneData.georgeState == GlobalSceneData.GeorgeState.Windows)
		{
			Debug.Log("GlobalSceneData.porchFixed: " + GlobalSceneData.porchFixed);
			StartThoughtEvent.SetActive(false);
			StartScrewEvent.SetActive(true);
			ScrewMinigame.SetActive(true);
			Tree.SetActive(false);
			Axe.SetActive(false);

			if (GlobalSceneData.porchFixed == GlobalSceneData.PorchFixed.Flat)
			{
				Debug.Log("Flat");
				FlatPorch.SetActive(true);
				PorchEvent.SetActive(false);
			}
			if (GlobalSceneData.porchFixed == GlobalSceneData.PorchFixed.Slanted)
			{
				Debug.Log("Slanted");
				SlantedPorch.SetActive(true);
				PorchEvent.SetActive(false);
			}

		}
	}
}
