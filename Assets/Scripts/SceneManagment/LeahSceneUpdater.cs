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
}
