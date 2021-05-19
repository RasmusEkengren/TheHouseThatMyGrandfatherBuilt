using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static bool paused = false;
    public static GameController instance;

    private static bool _tutorialDone = false;
    public static bool tutorialDone
    {
        get { return _tutorialDone; }
        private set { _tutorialDone = value; ControlsTutorial.ShowInteractionControls(false); }
    }

    public static bool _firstMovement = false;
    public static bool firstMovement
    {
        get { return _firstMovement; }
        set { _firstMovement = value; if (!tutorialDone && value == true) { ControlsTutorial.ShowMovementControls(true); } }
    }

    private static bool _firstInteraction = false;
    public static bool firstInteraction {
        get { return _firstInteraction; }
        set { _firstInteraction = value; if (value == true) { ControlsTutorial.ShowInteractionControls(true); } }
    }

    private static ControlsTutorial tutorial = null;

    private void Start()
    {
        Time.timeScale = 1;
        PauseGame(false);
        if (FindObjectOfType<ControlsTutorial>())
        {
            tutorial = FindObjectOfType<ControlsTutorial>();
            // ControlsTutorial.ShowMovementControls(true);
        }
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
        if (pause)
        {
            paused = true;
        }
        if (!pause)
        {
            paused = false;
        }
    }
}
