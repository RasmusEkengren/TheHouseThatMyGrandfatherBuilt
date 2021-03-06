using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private static bool paused = false;
	public static GameController instance;

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
			// Time.timeScale = 0;
			Debug.Log("Game Paused", gameObject);
		}
		if (!pause)
		{
			paused = false;
			// Time.timeScale = 1;
			Debug.Log("Game Unpaused", gameObject);
		}
	}
}
