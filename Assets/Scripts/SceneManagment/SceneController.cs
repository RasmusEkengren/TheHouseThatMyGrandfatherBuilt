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
        public GameObject transitionObject;

        // [FMODUnity.EventRef] public string transitionSound;

        public int sceneChangeDelay = 2;

        // Something to use?
        public static UnityAction<SceneController> onChange = delegate { };

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        #endregion Initializations

        #region PublicFunctions
        public IEnumerator LoadNextScene(string nextScene)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
            operation.allowSceneActivation = false;

            // While operation is loading
            transitionObject.SetActive(true);
            PlayVFX(0);

            yield return new WaitForSeconds(sceneChangeDelay);

            // Wait until done
            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;

                    PlayVFX(1);

                    // Unpause Game (Or do it at the start of every scene)
                }    
                yield return null;
            }
        }
        #endregion PublicFunctions

        #region PrivateFunctions
        private void PlayVFX(int fadeIndex)
        {
            // Should we have VFX references here on this script or on a separate one?
            if (fadeIndex <= 0)
            {
                Debug.Log("Playing fade in");
                animator.Play(fadeInClip);
            }

            if (fadeIndex >= 1)
            {
                Debug.Log("Playing fade out");
                animator.Play(fadeOutClip);
            }
        }

        private void PlaySFX()
        {
           // FMODUnity.RuntimeManager.PlayOneShot(transitionSound);
        }
        #endregion PrivateFunctions
    }
}
