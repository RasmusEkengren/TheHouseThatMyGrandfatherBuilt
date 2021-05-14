using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeahSceneUpdater : MonoBehaviour
{
	void Start()
	{
		foreach (ObjectState objectState in FindObjectsOfType<ObjectState>(true))
		{
			objectState.SetState();
		}
	}
	public void ActivatePorch()
	{
		GlobalSceneData.georgeState = GlobalSceneData.GeorgeState.Porch;
	}
	public void ActivateWindows()
	{
		GlobalSceneData.georgeState = GlobalSceneData.GeorgeState.Windows;
	}
	public void ActivateLeahEntering()
	{
		GlobalSceneData.leahState = GlobalSceneData.LeahState.Entering;
	}
	public void ActivateLeahBuilding()
	{
		GlobalSceneData.leahState = GlobalSceneData.LeahState.Building;
	}
	public void ActivateLeahDone()
	{
		GlobalSceneData.leahState = GlobalSceneData.LeahState.Done;
	}
}
