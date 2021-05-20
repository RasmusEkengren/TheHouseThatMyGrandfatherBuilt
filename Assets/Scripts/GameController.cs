using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static bool paused = false;
    public static GameController instance;

    private float delay = 1.5f;

    private bool tutorialFinished = false;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void StartTutorial()
    {
        StartCoroutine("startDelay");
    }
    public void FinishTutorial()
    {
        ControlsTutorial.ShowMovementControls(false);
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

        if (pause == false && tutorialFinished == false)
        {
            StartTutorial();
        }
    }
}
