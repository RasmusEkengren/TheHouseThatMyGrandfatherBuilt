using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
	void Start()
	{
		GlobalSceneData.LoadGame();
		if (GlobalSceneData.leahState == GlobalSceneData.LeahState.Entering && GlobalSceneData.interactedObjectIDs.Count <= 0)
		{
			this.GetComponent<Button>().interactable = false;
		}
		else
		{
			this.GetComponent<Button>().interactable = true;
		}
	}
}
