using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace Scene
{
    public class SceneChanger : MonoBehaviour
    {
        // This will be a lightweight script to put on objects with the sole purpose of changing the scene

        #region Initializations
        #if UNITY_EDITOR
        public SceneAsset nextScene = null; // Used to streamline scene assignment for designers in Unity
        #endif

        [HideInInspector]
        public string sceneString = null;

        private void Start()
        {
            if (sceneString == null)
            {
                Debug.LogWarning(gameObject.name + ": SceneChanger - No scene given");
            }
        }
        #endregion Initializations

        #region PublicFunctions
        public void ChangeScene()
        {
            // If the Scene exists in build settings
            if (SceneManager.GetSceneByName(sceneString).buildIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneController.instance.StartCoroutine(SceneController.instance.LoadNextScene(sceneString));

                Debug.Log("Changing Scene to: " + sceneString);

                // Pause game here
            }

            // Integrity check
            else
            {
                if (sceneString == null)
                {
                    Debug.LogWarning(gameObject.name + ": SceneChanger - No scene given");
                }
                else
                {
                    Debug.LogWarning(gameObject.name + ": SceneChanger - Scene (" + sceneString + ") is not loaded in build settings");
                }
            }
        }
        #endregion PublicFunctions
    }
}
