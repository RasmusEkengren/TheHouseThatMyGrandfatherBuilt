using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeListener : MonoBehaviour
{
	private GameObject player = null;
	private void Start()
	{
		SceneManager.sceneLoaded += onLoad;
	}

	private void onLoad(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("Scene loaded: " + scene.name + ", Leah state: " + GlobalSceneData.leahState);
		if (scene.name == "Leah")
		{
			LoadLeahPosition();
			if (GlobalSceneData.leahState != GlobalSceneData.LeahState.Entering)
			{
				LoadCameraPosition();
			}
		}

		if (scene.name == "George")
		{
			//if (GlobalSceneData.mg_porchFixed)
			//{
			//    GlobalSceneData.georgeState = GlobalSceneData.GeorgeState.Windows;
			//}
			//else
			//{
			//    GlobalSceneData.georgeState = GlobalSceneData.GeorgeState.Porch;
			//}

			// if (!GlobalSceneData.introDone)
			// {
			// 	GlobalSceneData.introDone = true;
			// }

			GlobalSceneData.SaveLeahPosition(FindObjectOfType<PlayerMovement>());
			GlobalSceneData.SaveCameraPosition(FindObjectOfType<CameraController>());
		}
	}

	public void LoadLeahPosition()
	{
		if (GlobalSceneData.leahState != GlobalSceneData.LeahState.Entering)
		{
			player = FindObjectOfType<PlayerMovement>().gameObject;
			//Disable and enable charcontroller between changing pos to make sure the characterController isn't reseting the position
			CharacterController charController = player.GetComponent<CharacterController>();
			charController.enabled = false;
			player.transform.position = GlobalSceneData.lastLeahPosition;
			player.transform.rotation = GlobalSceneData.lastLeahRotation;
			charController.enabled = true;
		}
	}
	public void LoadCameraPosition()
	{
		GameObject camera = FindObjectOfType<CameraController>().gameObject;
		camera.transform.position = GlobalSceneData.lastCameraPosition;
		camera.transform.rotation = GlobalSceneData.lastCameraRotation;
	}
}
