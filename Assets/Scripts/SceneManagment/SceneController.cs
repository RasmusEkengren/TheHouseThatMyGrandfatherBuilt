using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Events;
using FMODUnity;


public class SceneType
{
    public enum Scene { Leah, George };
    public Scene sceneType;
}

public class GeorgeState
{
    public enum State { Porch, Windows };
}

// This class will be the core hub for managing scenes
public class SceneController : MonoBehaviour
{
    #region Initializations
    public static SceneController instance;
    public SceneType.Scene sceneType;

    [SerializeField] private Animator animator;
    [SerializeField] private string fadeOutClip = null;
    [SerializeField] private string fadeInClip = null;
    [SerializeField] private GameObject transitionObject;

    [Space]
    [SerializeField] private GameEvent onSceneChange;
    [Space]

    [FMODUnity.EventRef] [SerializeField] private string transitionSound = null;

    [SerializeField] private int sceneChangeDelay = 2;
    private bool changingScene = false;

    private GameObject player = null;

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
    public IEnumerator LoadNextScene(string nextScene)
    {
        if (!changingScene)
        {
            changingScene = true;
            GameController.instance.PauseGame(true);

            transitionObject.SetActive(true);
            PlayVFX(0);
            PlaySFX();

            AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
            operation.allowSceneActivation = false;

            yield return new WaitForSeconds(sceneChangeDelay);

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    onSceneChange.Invoke(); // Placing this here to hide it behind transition (in case something is disabled in player sight)
                    if (sceneType == SceneType.Scene.Leah && !GameController.introDone)
                    {
                        GameController.introDone = true;
                    }

                    PlayVFX(1);
                    operation.allowSceneActivation = true;
                    GameController.instance.PauseGame(false);
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

    private void PlaySFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(transitionSound);
    }
    #endregion PrivateFunctions
}

