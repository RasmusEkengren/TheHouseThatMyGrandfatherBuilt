using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // This class will be the core hub for managing scenes

    public static SceneController instance;

    public IEnumerator LoadNextScene(Scene nextScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene.ToString());
        operation.allowSceneActivation = false;

        // While operation is loading

        // Play "fade in" VFX 

        // Wait
        while (!operation.isDone)
        {
            if (operation.progress >= 0.95f)
            {
                operation.allowSceneActivation = true;

                // Play "fade out" VFX

                // Unpause Game (Or do it at the start of every scene)
            }

            yield return null;
        }

        yield return null;
    }

    private void PlayVFX()
    {
        // Should we have VFX references here on this script or on a separate one?
    }
}
