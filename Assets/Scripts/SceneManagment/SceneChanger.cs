using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


// This will be a lightweight script to put on objects with the sole purpose of changing the scene
public class SceneChanger : MonoBehaviour
{
    #region Initializations
#if UNITY_EDITOR
    public SceneAsset nextScene = null; // Used to streamline scene assignment for designers in Unity
#endif

    public Color transitionColor = Color.white;
    [HideInInspector]
    public string sceneString = null;

    private void Start()
    {
        transitionColor.a = 0;
        if (sceneString == null)
        {
            Debug.LogWarning(gameObject.name + ": SceneChanger - No scene given", gameObject);
        }
        if (!SceneController.instance)
        {
            Debug.LogWarning(gameObject.scene.name + " has no SceneController, add it from Prefabs -> Scene Essentials");
        }
    }
    #endregion Initializations

    /// <summary>
    /// Call to change the scene to the specified scene
    /// </summary>
    #region PublicFunctions
    public void ChangeScene()
    {
        // If the Scene exists in build settings
        if (SceneManager.GetSceneByName(sceneString).buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneController.instance.StartCoroutine(SceneController.instance.LoadNextScene(sceneString, transitionColor));

            Debug.Log("Changing Scene to: " + sceneString, gameObject);

            // Pause game here
        }

        // Integrity check
        else
        {
            if (sceneString == null)
            {
                Debug.LogWarning(gameObject.name + ": SceneChanger - No scene given", gameObject);
            }
            else
            {
                Debug.LogWarning(gameObject.name + ": SceneChanger - Scene (" + sceneString + ") is not loaded in build settings", gameObject);
            }
        }
    }
    #endregion PublicFunctions
}

