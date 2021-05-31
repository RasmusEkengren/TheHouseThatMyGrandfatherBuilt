using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu = null;
	[SerializeField] private GameController gameController = null;
	private bool hasPaused = false;

    public void ToggleMenu(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed)
		{
			if (pauseMenu.activeSelf)
			{
				pauseMenu.SetActive(false);
				if (!hasPaused) gameController.PauseGame(false);
			}
			else
			{
				hasPaused = GameController.GamePaused();
				pauseMenu.SetActive(true);
				gameController.PauseGame(true);
			}
		}
	}
	public void Resume()
	{
		pauseMenu.SetActive(false);
		if (!hasPaused) gameController.PauseGame(false);
	}
	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();	
#endif
	}
}
