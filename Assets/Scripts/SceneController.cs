using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Events;

namespace Scene
{
    // This class will be the core hub for managing scenes
    public class SceneController : MonoBehaviour
    {
        #region Initializations
        public static SceneController instance;

        public Animator animator;
        public string fadeOutClip = null;
        public string fadeInClip = null;

        public int sceneChangeDelay = 2;

        // Something to use?
        public static UnityAction<SceneController> onChange = delegate { };

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
            animator.Play(fadeInClip);
            // Play transition VFX 

            yield return new WaitForSeconds(sceneChangeDelay);

            // Wait until done
            while (!operation.isDone)
            {
                if (operation.progress >= 0.8f)
                {
                    operation.allowSceneActivation = true;

                    // Play transition VFX

                    animator.Play(fadeOutClip);

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
