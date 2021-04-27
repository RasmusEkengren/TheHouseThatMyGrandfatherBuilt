using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrewParent : MonoBehaviour
{
	public GameObject screwingGamePrefab;
	private GameObject child;
	public void StartGame()
	{
		child = Instantiate(screwingGamePrefab, Vector3.zero, Quaternion.identity);
		child.transform.SetParent(this.gameObject.transform);
		child.GetComponent<RectTransform>().offsetMax = Vector2.zero;
		child.GetComponent<RectTransform>().offsetMin = Vector2.zero;
	}
	void Start()
	{
		StartGame();
	}
	public void OnMove(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		Vector2 val = value.ReadValue<Vector2>();
		if (child != null) child.GetComponent<Screwing>().Move(val);
	}
}
