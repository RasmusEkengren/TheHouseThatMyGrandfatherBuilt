using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static bool paused = false;
    public static GameController instance;

    private void Start()
    {
        Time.timeScale = 1;
        PauseGame(false);
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
