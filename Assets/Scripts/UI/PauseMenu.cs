using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu = null;
	[SerializeField] private GameController gameController = null;
	public void ToggleMenu(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed)
		{
			if (pauseMenu.activeSelf)
			{
				pauseMenu.SetActive(false);
				gameController.PauseGame(false);
			}
			else
			{
				pauseMenu.SetActive(true);
				gameController.PauseGame(true);
			}
		}
	}
	public void Resume()
	{
		pauseMenu.SetActive(false);
		gameController.PauseGame(false);
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
