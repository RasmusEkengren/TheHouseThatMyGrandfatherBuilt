using UnityEngine;
using System.Collections;

public class QuitGameOnKeypress : MonoBehaviour {
	public void OnCancel () {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}