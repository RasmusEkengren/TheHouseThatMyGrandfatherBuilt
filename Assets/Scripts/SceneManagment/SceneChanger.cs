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

    [Tooltip("Delay of scene change after door sounds have been played (if checked)")]
    [SerializeField] private float sceneChangeDelay = 2;
    [SerializeField] private bool playVFX = true;
    public Color transitionColor;
    [FMODUnity.EventRef] [SerializeField] private string transitionSound = null;
    [HideInInspector]
    public string sceneString = null;

    [Tooltip("Plays door sound sequence (3 seconds) before Transition Sound")]
    public bool PlayDoorSounds = false;

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
            SceneController.instance.StartCoroutine(SceneController.instance.LoadNextScene(sceneString, sceneChangeDelay, playVFX, transitionColor, transitionSound, PlayDoorSounds));

            Debug.Log("Changing Scene to: " + sceneString + " , fading to " + transitionColor, gameObject);

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

