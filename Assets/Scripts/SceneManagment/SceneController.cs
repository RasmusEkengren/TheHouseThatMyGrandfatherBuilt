using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Events;
using FMODUnity;

// This class will be the core hub for managing scenes
public class SceneController : MonoBehaviour
{
    #region Initializations
    public static SceneController instance;

    [SerializeField] private Animator animator;
    [SerializeField] private string fadeOutClip = null;
    [SerializeField] private string fadeInClip = null;
    [SerializeField] private GameObject transitionObject;
    private GameController gameController;

    private bool changingScene = false;

    [FMODUnity.EventRef] [SerializeField] private string doorOpen = null;
    [FMODUnity.EventRef] [SerializeField] private string doorClose = null;

    private GameObject musicObject = null;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
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
    public IEnumerator LoadNextScene(string nextScene, float delay, bool VFX, Color transitionColor, string transitionSound, bool doorSound)
    {
        if (!changingScene)
        {
            changingScene = true;

            if (FindObjectOfType<MusicObject>())
            {
                musicObject = FindObjectOfType<MusicObject>().gameObject;
                musicObject.SetActive(false);
            }

            if (gameController != null)
            {
                gameController = FindObjectOfType<GameController>();
                gameController.PauseGame(true);
            }
            transitionColor.a = 0;
            transitionObject.GetComponent<Image>().color = transitionColor;
            transitionObject.SetActive(true);
            if (VFX) PlayVFX(0);
            if (doorSound)
            {
                yield return new WaitForSeconds(0.2f);
                FMODUnity.RuntimeManager.PlayOneShot(doorOpen);
                yield return new WaitForSeconds(1.3f);

                FMODUnity.RuntimeManager.PlayOneShot(doorClose);
                yield return new WaitForSeconds(1.7f);
            }
            PlaySFX(transitionSound);

            AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
            operation.allowSceneActivation = false;

            yield return new WaitForSeconds(delay);

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    PlayVFX(1);
                    operation.allowSceneActivation = true;
                    if (gameController != null) gameController.PauseGame(false);
                    changingScene = false;
                }
                yield return null;
            }
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

    private void PlaySFX(string transitionSound)
    {
        FMODUnity.RuntimeManager.PlayOneShot(transitionSound);
    }
    #endregion PrivateFunctions
}

