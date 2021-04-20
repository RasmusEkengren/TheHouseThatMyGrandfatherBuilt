using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
	[SerializeField] private EventSystem eventSystem;
	private Button button;
	void OnEnable()
	{
		button = GetComponent<Button>();
		eventSystem.SetSelectedGameObject(null);
		button.Select();
		button.OnSelect(null);
	}
}
