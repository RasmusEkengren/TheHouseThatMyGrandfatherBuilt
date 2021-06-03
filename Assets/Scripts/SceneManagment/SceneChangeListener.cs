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
		if (scene.name == "Leah" && GlobalSceneData.leahState != GlobalSceneData.LeahState.Entering)
		{
			LoadLeahPosition();
			LoadCameraPosition();
		}

		/*if (scene.name == "George")
        {

            GlobalSceneData.SaveLeahPosition(FindObjectOfType<PlayerMovement>());
            GlobalSceneData.SaveLeahCameraPosition(FindObjectOfType<CameraController>());
        }*/
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
