using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static bool paused = false;
    public GameController instance;

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
        Time.timeScale = 1;
    }

    public static bool GamePaused()
    {
        return paused;
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            paused = true;
            Time.timeScale = 0;
        }
        if (!pause)
        {
            paused = false;
            Time.timeScale = 1;
        }
    }
}
