using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	public static Vector3 lastCameraPosition;
	public static Quaternion lastCameraRotation;
	public SceneType sceneType;
	//public List<DataContainer> containerList = new List<DataContainer>();

	public static bool mg_porchFixed; /*{ get { return mg_porchFixed; } private set { mg_porchFixed = value; } }*/
	public static bool mg_windowsFixed;
	public static bool mg_fenceFixed;
	//private bool leahPositionUpdated = false;

	public enum LeahState { Entering, Building, Done }
	public static LeahState leahState = LeahState.Entering;

	public enum GeorgeState { Porch, Windows, Fence }
	public static GeorgeState georgeState;

	public enum PorchFixed { None, Flat, Slanted }
	public static PorchFixed porchFixed = PorchFixed.None;
	public static List<string> interactedObjectIDs = new List<string>();

	private GameObject player;

	void Update()
	{
		//if (mg_windowsFixed) Debug.Log("Yay once again!");
	}

	public static void SaveLeahPosition(PlayerMovement player)
	{
		lastLeahPosition = player.transform.position;
		lastLeahRotation = player.transform.rotation;
	}
	public static void SaveCameraPosition(CameraController camera)
	{
		lastCameraPosition = camera.transform.position;
		lastCameraRotation = camera.transform.rotation;
	}
	public static bool FindInteractedState(string _id)
	{
		foreach (string id in interactedObjectIDs)
		{
			if (id == _id) return true;
		}
		return false;
	}
}
