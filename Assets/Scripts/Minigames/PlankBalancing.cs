using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlankBalancing : MonoBehaviour
{
	[SerializeField] private RectTransform greenPip = null;
	[SerializeField] private RectTransform redZone1 = null;
	[SerializeField] private RectTransform redZone2 = null;
	[SerializeField] private RectTransform arrow = null;
	[SerializeField] private float moveSpeed = 100f;
	[SerializeField] private float fallLimit = 4f;
	[SerializeField] private float[] intervalRange = new float[2];
	[SerializeField] private float[] speedRange = new float[2];
	[SerializeField] private UnityEvent fallEvents = null;
	private Vector2 moveVal = Vector2.zero;
	private float offBalance = 0f;
	private float timer = 0f;
	private float currentInterval = 0;
	private float currentSpeed = 0;
	private int moveDir = 0;
	private int sameDir = 0;
	public void OnInput(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		moveVal = value.ReadValue<Vector2>();
	}
	public void ResetGame()
	{
		offBalance = 0f;
		timer = 0f;
		greenPip.localPosition = new Vector3(greenPip.localPosition.x, 0f, greenPip.localPosition.z);
		ChangeSpeed();
	}
	private void ChangeSpeed()
	{
		currentInterval = Random.Range(intervalRange[0], intervalRange[1]);
		currentSpeed = Random.Range(speedRange[0], speedRange[1]);
		int lastDir = moveDir;
		if (Random.Range(0, 2) == 0) moveDir = 1;
		else moveDir = -1;
		if (lastDir == moveDir) sameDir++;
		if (sameDir >= 2)
		{
			if (moveDir == 1) moveDir = -1;
			else moveDir = 1;
		}
		arrow.localScale = new Vector3(1, -moveDir, 1);
	}
	void OnEnable()
	{
		ResetGame();
	}
	void Update()
	{
		timer += Time.deltaTime;
		if (timer > currentInterval)
		{
			ChangeSpeed();
			timer = 0f;
		}
		greenPip.localPosition += Vector3.up * Time.deltaTime * currentSpeed * moveDir;
		greenPip.localPosition += new Vector3(0f, moveVal.y, 0f) * Time.deltaTime * moveSpeed;
		if (greenPip.localPosition.y > redZone1.localPosition.y) greenPip.localPosition = new Vector3(greenPip.localPosition.x, redZone1.localPosition.y, greenPip.localPosition.z);
		else if (greenPip.localPosition.y < redZone2.localPosition.y) greenPip.localPosition = new Vector3(greenPip.localPosition.x, redZone2.localPosition.y, greenPip.localPosition.z);
		if (greenPip.localPosition.y + greenPip.sizeDelta.y * 0.5f > redZone1.localPosition.y - redZone1.sizeDelta.y * 0.5f ||
			greenPip.localPosition.y - greenPip.sizeDelta.y * 0.5f < redZone2.localPosition.y + redZone1.sizeDelta.y * 0.5f)
		{
			offBalance += Time.deltaTime;
		}
		else if (offBalance > 0f)
		{
			offBalance -= Time.deltaTime;
		}
		if (offBalance > fallLimit)
		{
			fallEvents.Invoke();
			ResetGame();
		}
		Debug.Log(offBalance);
	}
}
