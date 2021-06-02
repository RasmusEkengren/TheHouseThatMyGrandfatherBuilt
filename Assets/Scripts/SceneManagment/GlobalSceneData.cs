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

	private PlayerMovement player;

	public static bool tutorialFinished = false;
	public static bool pickedUpPictureFrame = false;

	private bool loadPositions = false;

	private void Start()
	{
		if (SceneManager.GetActiveScene().name == "George" || SceneManager.GetActiveScene().name == "Inside") { tutorialFinished = true; ControlsTutorial.ShowMovementControls(false); }
		SceneManager.sceneLoaded += OnSceneLoaded;
		// Ugly solution for the Tester Helper tool
		lastLeahPosition = new Vector3(18f, 1f, -38f);
		lastCameraPosition = new Vector3(20f, 2f, -36f);
	}

	// When message from SceneController has been recieved
	public void OnChangingScene(string nextScene)
	{
		string currentScene = SceneManager.GetActiveScene().name;

		// If Leah to George or Leah to Leah, save positions
		if (currentScene == "Leah" && nextScene == "George" || currentScene == "Leah" && nextScene == "Leah")
		{
			PlayerMovement player = FindObjectOfType<PlayerMovement>();
			CameraController camera = FindObjectOfType<CameraController>();

			SaveLeahPosition(player);
			SaveLeahCameraPosition(camera);
		}

		// If George to Leah or Leah to Leah, load positions
		if (currentScene == "George" && nextScene == "Leah" || currentScene == "Leah" && nextScene == "Leah")
		{
			loadPositions = true;
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (loadPositions)
		{
			PlayerMovement player = FindObjectOfType<PlayerMovement>();
			CameraController camera = FindObjectOfType<CameraController>();

			player.gameObject.transform.position = lastLeahPosition;
			player.gameObject.transform.rotation = lastLeahRotation;

			camera.gameObject.transform.position = lastCameraPosition;
			camera.gameObject.transform.rotation = lastCameraRotation;
		}
	}

	public static void SaveLeahPosition(PlayerMovement player)
	{
		lastLeahPosition = player.transform.position;
		lastLeahRotation = player.transform.rotation;

	}
	public static void SaveLeahCameraPosition(CameraController camera)
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
	public static void SaveGame()
	{
		PlayerPrefs.SetFloat("LeahPosX", lastLeahPosition.x);
		PlayerPrefs.SetFloat("LeahPosY", lastLeahPosition.y);
		PlayerPrefs.SetFloat("LeahPosZ", lastLeahPosition.z);
		PlayerPrefs.SetFloat("LeahRotX", lastLeahRotation.x);
		PlayerPrefs.SetFloat("LeahRotY", lastLeahRotation.y);
		PlayerPrefs.SetFloat("LeahRotZ", lastLeahRotation.z);
		PlayerPrefs.SetFloat("LeahRotW", lastLeahRotation.w);
		PlayerPrefs.SetFloat("CamPosX", lastLeahPosition.x);
		PlayerPrefs.SetFloat("CamPosY", lastLeahPosition.y);
		PlayerPrefs.SetFloat("CamPosZ", lastLeahPosition.z);
		PlayerPrefs.SetFloat("CamRotX", lastLeahRotation.x);
		PlayerPrefs.SetFloat("CamRotY", lastLeahRotation.y);
		PlayerPrefs.SetFloat("CamRotZ", lastLeahRotation.z);
		PlayerPrefs.SetFloat("CamRotW", lastLeahRotation.w);
		PlayerPrefs.SetInt("TutorialFinished", tutorialFinished ? 1 : 0);
		PlayerPrefs.SetInt("pickedUpPictureFrame", pickedUpPictureFrame ? 1 : 0);
		PlayerPrefs.SetInt("leahState", ((int)leahState));
		PlayerPrefs.SetInt("georgeState", ((int)georgeState));
		PlayerPrefs.SetInt("porchFixingState", ((int)porchFixingState));
		PlayerPrefs.SetInt("porchStyle", ((int)porchStyle));
		PlayerPrefs.SetInt("windowsFixingState", ((int)windowsFixingState));
		PlayerPrefs.SetInt("windowsStyle", ((int)windowsStyle));
		PlayerPrefs.SetInt("railingFixingState", ((int)railingFixingState));
		PlayerPrefs.SetInt("railingStyle", ((int)railingStyle));
		int i = 0;
		foreach (string id in interactedObjectIDs)
		{
			PlayerPrefs.SetString("ID" + i.ToString(), id);
			i++;
		}
		PlayerPrefs.Save();
	}
	public static void LoadGame()
	{
		if (!PlayerPrefs.HasKey("leahState"))
		{
			Debug.LogWarning("No Saved Game Found");
			return;
		}
		else
		{
			lastLeahPosition = new Vector3(PlayerPrefs.GetFloat("LeahPosX"), PlayerPrefs.GetFloat("LeahPosY"), PlayerPrefs.GetFloat("LeahPosZ"));
			lastLeahRotation = new Quaternion(PlayerPrefs.GetFloat("LeahRotX"), PlayerPrefs.GetFloat("LeahRotY"), PlayerPrefs.GetFloat("LeahRotZ"), PlayerPrefs.GetFloat("LeahRotW"));
			lastCameraPosition = new Vector3(PlayerPrefs.GetFloat("CamPosX"), PlayerPrefs.GetFloat("CamPosY"), PlayerPrefs.GetFloat("CamPosZ"));
			lastCameraRotation = new Quaternion(PlayerPrefs.GetFloat("CamRotX"), PlayerPrefs.GetFloat("CamRotY"), PlayerPrefs.GetFloat("CamRotZ"), PlayerPrefs.GetFloat("CamRotW"));
			if (PlayerPrefs.GetInt("TutorialFinished") == 0) tutorialFinished = false;
			else if (PlayerPrefs.GetInt("TutorialFinished") == 1) tutorialFinished = true;
			if (PlayerPrefs.GetInt("pickedUpPictureFrame") == 0) pickedUpPictureFrame = false;
			else if (PlayerPrefs.GetInt("pickedUpPictureFrame") == 1) pickedUpPictureFrame = true;
			leahState = (LeahState)PlayerPrefs.GetInt("leahState");
			georgeState = (GeorgeState)PlayerPrefs.GetInt("georgeState");
			porchFixingState = (PorchFixingState)PlayerPrefs.GetInt("porchFixingState");
			porchStyle = (PorchStyle)PlayerPrefs.GetInt("porchStyle");
			windowsFixingState = (WindowsFixingState)PlayerPrefs.GetInt("windowsFixingState");
			windowsStyle = (WindowsStyle)PlayerPrefs.GetInt("windowsStyle");
			railingFixingState = (RailingFixingState)PlayerPrefs.GetInt("railingFixingState");
			railingStyle = (RailingStyle)PlayerPrefs.GetInt("railingStyle");
			int i = 0;
			bool hasData = true;
			while (hasData)
			{
				if (!PlayerPrefs.HasKey("ID" + i.ToString()))
				{
					hasData = false;
					break;
				}
				interactedObjectIDs.Add(PlayerPrefs.GetString("ID" + i.ToString()));
				i++;
			}
		}
	}
	public static void ResetEverything()
	{
		lastLeahPosition = new Vector3(-7.43100023f, 1.06999969f, 59.2550011f);
		lastLeahRotation = new Quaternion(0f, -0.952481925f, 0f, 0.304595262f);
		lastCameraPosition = new Vector3(-9.40624046f, 4.99852419f, 61.3174095f);
		lastCameraRotation = new Quaternion(0.176703542f, 0.819491208f, -0.426600128f, 0.339444369f);
		tutorialFinished = false;
		pickedUpPictureFrame = false;
		leahState = LeahState.Entering;
		georgeState = GeorgeState.Porch;
		porchFixingState = PorchFixingState.Broken;
		porchStyle = PorchStyle.None;
		windowsFixingState = WindowsFixingState.Broken;
		windowsStyle = WindowsStyle.None;
		railingFixingState = RailingFixingState.Broken;
		railingStyle = RailingStyle.None;
		interactedObjectIDs.Clear();
		PlayerPrefs.DeleteAll();
	}
}
