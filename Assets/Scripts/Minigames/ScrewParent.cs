using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ScrewParent : MonoBehaviour
{
	[SerializeField] private UnityEvent endEvent = null;
	[SerializeField] private int numberOfGamesToComplete = 1;
	[SerializeField] private float slideTime = 1;
	[SerializeField] private float slideSpeed = 1.5f;

	public GameObject screwingGamePrefab;
	private int gameCompletions = 0;
	private GameObject child;
	public void StartGame()
	{
		child = Instantiate(screwingGamePrefab, Vector3.zero, Quaternion.identity);
		child.transform.SetParent(this.gameObject.transform);
		child.GetComponent<RectTransform>().offsetMax = Vector2.zero;
		child.GetComponent<RectTransform>().offsetMin = Vector2.zero;
	}
	public void NextGame()
	{
		gameCompletions++;
		if (gameCompletions >= numberOfGamesToComplete)
		{
			Destroy(child);
			endEvent.Invoke();
			return;
		}
		StartCoroutine(SlideStart());
	}
	public void OnMove(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		Vector2 val = value.ReadValue<Vector2>();
		if (child != null) child.GetComponent<Screwing>().Move(val);
	}
	private IEnumerator SlideStart()
	{
		float t = 0;
		RectTransform childTransform = child.GetComponent<RectTransform>();
		while (t <= slideTime)
		{
			Debug.Log(t);
			childTransform.anchorMax = new Vector2(childTransform.anchorMax.x - slideSpeed * Time.deltaTime, childTransform.anchorMax.y);
			childTransform.anchorMin = new Vector2(childTransform.anchorMin.x - slideSpeed * Time.deltaTime, childTransform.anchorMin.y);
			t += Time.deltaTime;
			yield return null;
		}
		Destroy(child);
		StartGame();
		yield return null;
	}
}
