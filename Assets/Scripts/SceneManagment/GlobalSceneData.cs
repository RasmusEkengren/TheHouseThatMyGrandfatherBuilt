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

	public enum LeahState { Entering, Building, Done }
	public static LeahState leahState = LeahState.Entering;

	public enum GeorgeState { Porch, Windows, Railing }
	public static GeorgeState georgeState;

	public enum PorchStyle { None, Flat, Slanted }
	public static PorchStyle porchStyle = PorchStyle.None;
	public enum PorchFixingState { Broken, Fixing, Fixed }
	public static PorchFixingState porchFixingState = PorchFixingState.Broken;

	public enum WindowsStyle { None, Ribbed, Solid }
	public static WindowsStyle windowsStyle = WindowsStyle.None;
	public enum WindowsFixingState { Broken, Fixing, Fixed }
	public static WindowsFixingState windowsFixingState = WindowsFixingState.Broken;

	public enum RailingStyle { None, FlatTop, Pillars }
	public static RailingStyle railingStyle = RailingStyle.None;
	public enum RailingFixingState { Broken, Fixing, Fixed }
	public static RailingFixingState railingFixingState = RailingFixingState.Broken;
	public static List<string> interactedObjectIDs = new List<string>();

	private GameObject player;

	public static bool tutorialFinished = false;

	private void Start()
	{
		if (SceneManager.GetActiveScene().name == "George" || SceneManager.GetActiveScene().name == "Inside") { tutorialFinished = true; }
		//SceneManager.sceneLoaded += OnSceneLoaded;
		// Ugly solution for the Tester Helper tool
		lastLeahPosition = new Vector3(18f, 1f, -38f);
		lastCameraPosition = new Vector3(20f, 2f, -36f);
	}

	// 		/Not sure if this is Needed?
	// private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	// {
	// 	if (porchState == PorchState.Broken)
	// 	{
	// 		mg_porchFixed = false;
	// 	}
	// 	else
	// 	{
	// 		mg_porchFixed = true;
	// 	}
	// }

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

    public void ResetEverything()
    {
        Debug.Log("RESET EVERYTHING!!!!!!!");
        Debug.Log("RESET EVERYTHING!!!!!!!");
        Debug.Log("RESET EVERYTHING!!!!!!!");
        Debug.Log("RESET EVERYTHING!!!!!!!");
        Debug.Log("RESET EVERYTHING!!!!!!!");
        Debug.Log("RESET EVERYTHING!!!!!!!");
        leahState = LeahState.Entering;
        georgeState = GeorgeState.Porch;
        porchFixingState = PorchFixingState.Broken;
        porchStyle = PorchStyle.None;
        windowsFixingState = WindowsFixingState.Broken;
        windowsStyle = WindowsStyle.None;
        railingFixingState = RailingFixingState.Broken;
        railingStyle = RailingStyle.None;
    }
}
