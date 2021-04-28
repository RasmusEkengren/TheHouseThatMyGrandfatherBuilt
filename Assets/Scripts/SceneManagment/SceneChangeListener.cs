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
        if (scene.name == "Leah")
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
            player.transform.position = GlobalSceneData.lastLeahPosition;
            player.transform.rotation = GlobalSceneData.lastLeahRotation;
        }

        if (scene.name == "George")
        {

        }
    }
}
