using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSceneData : MonoBehaviour
{
	//[Serializable]
	//public class DataContainer
	//{
	//	public string name = null;
	//	public GeorgeState.State georgeState = GeorgeState.State.Porch;
	//	public GameObject obj;
	//}

	public static Vector3 lastLeahPosition;
	public static Quaternion lastLeahRotation;
	public SceneType sceneType;
	//public List<DataContainer> containerList = new List<DataContainer>();

	public static bool mg_porchFixed;
	public static bool mg_windowsFixed;
	public static bool tutorialDone;
	public static bool introDone = false;

	void Update()
	{
		if (mg_windowsFixed) Debug.Log("Yay once again!");
	}

    public void SavePlayerPosition()
    {

    }

	/// Health component som man kan binda events till
	/// T�nk inte p� det f�r mycket
	/// Avbinda events
	/// ?.
}
