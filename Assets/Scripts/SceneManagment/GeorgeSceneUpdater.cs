using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeSceneUpdater : MonoBehaviour
{
	private void Start()
	{
		foreach (ObjectState objectState in FindObjectsOfType<ObjectState>(true))
		{
			objectState.SetState();
		}
	}
	public void ActivateWindowsFixed()
	{
		GlobalSceneData.mg_windowsFixed = true;
	}
	public void ActivateLeahDone()
	{
		GlobalSceneData.leahState = GlobalSceneData.LeahState.Done;
	}
}
