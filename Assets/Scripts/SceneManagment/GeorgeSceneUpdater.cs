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
	public void ActivateRailingFixed()
	{
		GlobalSceneData.mg_railingFixed = true;
	}
	public void CheckIfLeahDone()
	{
		if (GlobalSceneData.mg_porchFixed && GlobalSceneData.mg_railingFixed && GlobalSceneData.mg_windowsFixed)
		{
			GlobalSceneData.leahState = GlobalSceneData.LeahState.Done;
		}
	}
}
