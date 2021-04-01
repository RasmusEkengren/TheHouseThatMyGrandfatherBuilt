using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenChanger : MonoBehaviour
{
    // This will be a lightweight script to put on objects with the sole purpose of changing the scene

    public Scene nextScene;

    public void ChangeScene()
    {
        // If the scene exists
        if (nextScene == SceneManager.GetSceneByName(nextScene.name))
        {
            SceneController.instance.StartCoroutine(SceneController.instance.LoadNextScene(nextScene));

            Debug.Log("Changing Scene to: " + nextScene.name);

            // Pause game
        }

        else
        {
            if (nextScene == null)
            {
                Debug.LogError(gameObject.name + ": SceneChanger - No scene specified");
            }
            else
            {
                Debug.LogError(gameObject.name + ": SceneChanger - Scene does not exist");
            }
        }
    }
}
