using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Scene
{
    public class SceneController : MonoBehaviour
    {
        // This class will be the core hub for managing scenes
        #region Initializations
        public static SceneController instance;

        private void Start()
        {
            instance = this;
        }
        #endregion Initializations

        #region PublicFunctions
        public IEnumerator LoadNextScene(string nextScene)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
            operation.allowSceneActivation = false;

            // While operation is loading

            // Play transition VFX 

            // Wait until done
            while (!operation.isDone)
            {
                if (operation.progress >= 0.8f)
                {
                    operation.allowSceneActivation = true;

                    // Play transition VFX

                    // Unpause Game (Or do it at the start of every scene)
                }

                yield return null;
            }
        }
        #endregion PublicFunctions

        #region PrivateFunctions
        private void PlayVFX()
        {

            // Should we have VFX references here on this script or on a separate one?
        }
        #endregion PrivateFunctions
    }
}
