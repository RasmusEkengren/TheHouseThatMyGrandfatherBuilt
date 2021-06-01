using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static bool paused = false;
    public static GameController instance;

    private float delay = 0.5f;

    public static Interactable lastInteraction = null;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Leah")
        {
            FinishTutorial();
        }
        Time.timeScale = 1;
        PauseGame(false);
    }

    public void StartTutorial()
    {
        StartCoroutine("startDelay");
    }
    public void FinishTutorial()
    {
        ControlsTutorial.ShowMovementControls(false);
        GlobalSceneData.tutorialFinished = true;
    }

    private IEnumerator startDelay()
    {
        yield return new WaitForSeconds(delay);
        ControlsTutorial.ShowMovementControls(true);
    }

    public static bool GamePaused()
    {
        return paused;
    }

    /// <summary>
    /// Call with a bool state to pause the game
    /// </summary>
    public void PauseGame(bool pause)
    {
        paused = pause;
        Debug.Log("Game paused: " + pause);

        if (pause == false && GlobalSceneData.tutorialFinished == false && GlobalSceneData.leahState == GlobalSceneData.LeahState.Entering)
        {
            StartTutorial();
        }
    }

    public void ResetLastInteraction()
    {
        lastInteraction.ResetInteraction();
    }
}
