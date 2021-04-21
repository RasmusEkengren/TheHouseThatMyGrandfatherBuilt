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

// This class will be the core hub for managing scenes
public class SceneController : MonoBehaviour
{
    #region Initializations
    public static SceneController instance;
    public SceneType.Scene sceneType;
    //[Tooltip("Level of the current scene type, starting from 0")]
    //[Range(0, 5)]
    public static int currentSceneLevel = 0;
    private static int leahSceneLevel = 0;
    private static int georgeSceneLevel = 0;

    [SerializeField] private Animator animator;
    [SerializeField] private string fadeOutClip = null;
    [SerializeField] private string fadeInClip = null;
    [SerializeField] private GameObject transitionObject;

    [FMODUnity.EventRef] [SerializeField] private string transitionSound = null;

    [SerializeField] private int sceneChangeDelay = 2;

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
        Debug.Log("Current scene levels --- Leah: " + leahSceneLevel + " || George: " + georgeSceneLevel);
        // SceneManager.sceneLoaded += OnSceneLoaded();
    }
    #endregion Initializations

    #region PublicFunctions
    public IEnumerator LoadNextScene(string nextScene)
    {
        //GameController.instance.PauseGame(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        PlaySFX();
        GameController.instance.PauseGame(true);
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
                PlayVFX(1);
                currentSceneLevel += 1;
                if (sceneType == SceneType.Scene.Leah) leahSceneLevel += 1;
                if (sceneType == SceneType.Scene.George) georgeSceneLevel += 1;
                Debug.Log("Current scene levels --- Leah: " + leahSceneLevel + " || George: " + georgeSceneLevel);
                operation.allowSceneActivation = true;

                // Unpause Game (Or do it at the start of every scene)
            }
            yield return null;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

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

