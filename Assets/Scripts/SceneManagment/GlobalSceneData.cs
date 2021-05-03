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
	public SceneType sceneType;
	//public List<DataContainer> containerList = new List<DataContainer>();

	public static bool mg_porchFixed /*{ get { return mg_porchFixed; } private set { mg_porchFixed = value; } }*/;
	public static bool mg_windowsFixed;
	public static bool tutorialDone = false;
	public static bool introDone = false;
	private bool leahPositionUpdated = false;

	public enum GeorgeState { Porch, Windows }
	public static GeorgeState georgeState;

	public enum PorchFixed { None, Flat, Slanted }
	public static PorchFixed porchFixed = PorchFixed.None;

	private GameObject player;

	void Update()
	{
		if (mg_windowsFixed) Debug.Log("Yay once again!");
	}

	public static void SaveLeahPosition(PlayerMovement player)
	{
		lastLeahPosition = player.transform.position;
		lastLeahRotation = player.transform.rotation;
	}

	/// Health component som man kan binda events till
	/// T�nk inte p� det f�r mycket
	/// Avbinda events
	/// ?.
}
