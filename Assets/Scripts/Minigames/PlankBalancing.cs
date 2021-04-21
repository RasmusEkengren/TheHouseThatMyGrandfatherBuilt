using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using FMOD.Studio;
using FMODUnity;

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
	[SerializeField] [EventRef] protected string plankBalancingSound = null;
	private string plankShakeParameter = "ShakeLevel";
	private EventInstance balancingSoundInstance;
	public void OnInput(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		moveVal = value.ReadValue<Vector2>();
	}
	public void ResetGame()
	{
		balancingSoundInstance.start();
		offBalance = 0f;
		timer = 0f;
		greenPip.localPosition = new Vector3(0f, greenPip.localPosition.y, greenPip.localPosition.z);
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
		arrow.localScale = new Vector3(1, moveDir, 1);
	}
	void OnEnable()
	{
		ResetGame();
		balancingSoundInstance = RuntimeManager.CreateInstance(plankBalancingSound);
		balancingSoundInstance.start();
	}
	void OnDisable()
	{
		balancingSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		balancingSoundInstance.release();
	}
	void Update()
	{
		timer += Time.deltaTime;
		if (timer > currentInterval)
		{
			ChangeSpeed();
			timer = 0f;
		}
		greenPip.localPosition += Vector3.right * Time.deltaTime * currentSpeed * moveDir;
		greenPip.localPosition += new Vector3(moveVal.x, 0f, 0f) * Time.deltaTime * moveSpeed;
		if (greenPip.localPosition.x < redZone1.localPosition.x) greenPip.localPosition = new Vector3(redZone1.localPosition.x, greenPip.localPosition.y, greenPip.localPosition.z);
		else if (greenPip.localPosition.x > redZone2.localPosition.x) greenPip.localPosition = new Vector3(redZone2.localPosition.x, greenPip.localPosition.y, greenPip.localPosition.z);
		if (greenPip.localPosition.x - greenPip.sizeDelta.x * 0.5f < redZone1.localPosition.x + redZone1.sizeDelta.x * 0.5f ||
			greenPip.localPosition.x + greenPip.sizeDelta.x * 0.5f > redZone2.localPosition.x - redZone1.sizeDelta.x * 0.5f)
		{
			offBalance += Time.deltaTime;
		}
		else if (offBalance > 0f)
		{
			offBalance -= Time.deltaTime;
		}
		float parameterValue = (Mathf.Abs(greenPip.localPosition.x) / Mathf.Abs(redZone1.localPosition.x)) * 2f;
		balancingSoundInstance.setParameterByName(plankShakeParameter, parameterValue);
		balancingSoundInstance.getParameterByName(plankShakeParameter, out parameterValue);
		Debug.Log(parameterValue);
		if (offBalance > fallLimit)
		{
			balancingSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			fallEvents.Invoke();
			ResetGame();
		}
		Debug.Log(offBalance);
	}
}
