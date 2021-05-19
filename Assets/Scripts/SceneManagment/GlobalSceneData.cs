using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneData : MonoBehaviour
{
	public static Vector3 lastLeahPosition;
	public static Quaternion lastLeahRotation;
	public static Vector3 lastCameraPosition;
	public static Quaternion lastCameraRotation;

	public static bool mg_porchFixed;
	public static bool mg_windowsFixed;
	public static bool mg_railingFixed;

	public enum LeahState { Entering, Building, Done }
	public static LeahState leahState = LeahState.Entering;

	public enum GeorgeState { Porch, Windows, Railing }
	public static GeorgeState georgeState;

	public enum PorchState { Broken, Flat, Slanted }
	public static PorchState porchState = PorchState.Broken;

	public enum WindowsState { Broken, Ribbed, Solid }
	public static WindowsState windowsState = WindowsState.Broken;

	public enum RailingState { Broken, FlatTop, Pillars }
	public static RailingState railingState = RailingState.Broken;
	public static List<string> interactedObjectIDs = new List<string>();

	private GameObject player;

	private void Start()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		// Ugly solution for the Tester Helper tool
		lastLeahPosition = new Vector3(18f, 1f, -38f);
		lastCameraPosition = new Vector3(20f, 2f, -36f);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (porchState == PorchState.Broken)
		{
			mg_porchFixed = false;
		}
		else
		{
			mg_porchFixed = true;
		}
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
