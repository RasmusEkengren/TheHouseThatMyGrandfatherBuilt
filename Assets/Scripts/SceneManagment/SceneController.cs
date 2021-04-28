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
        player = GetComponent<PlayerMovement>().gameObject;
        // SceneManager.sceneLoaded += OnSceneLoaded();
    }
    #endregion Initializations

	#region PublicFunctions
	public IEnumerator LoadNextScene(string nextScene)
	{
		if (!changingScene)
		{
			//GameController.instance.PauseGame(true);
			AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
			PlaySFX();
			GameController.instance.PauseGame(true);

			GlobalSceneData.lastLeahPosition = player.transform.position;
			GlobalSceneData.lastLeahRotation = player.transform.rotation;

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
                    if (sceneType == SceneType.Scene.Leah && !GameController.introDone)
                    {
                        GameController.introDone = true;
                    }
                    PlayVFX(1);
                    operation.allowSceneActivation = true;

					// Unpause Game (Or do it at the start of every scene)
					GameController.instance.PauseGame(false);
				}
				yield return null;
			}
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

