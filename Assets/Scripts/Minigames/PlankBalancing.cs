using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using FMOD.Studio;
using FMODUnity;

public class PlankBalancing : MonoBehaviour
{
	private class GreenZone
	{
		private Vector2 greenStartMin;
		private Vector2 greenStartMax;
		private Vector2 size;
		private RectTransform greenPip;
		public GreenZone(ref RectTransform greenPip)
		{
			greenStartMin = greenPip.anchorMin;
			greenStartMax = greenPip.anchorMax;
			this.greenPip = greenPip;
			size = new Vector2(greenPip.anchorMax.x - greenPip.anchorMin.x, greenPip.anchorMax.y - greenPip.anchorMin.y);
		}
		public void Reset()
		{
			greenPip.anchorMin = greenStartMin;
			greenPip.anchorMax = greenStartMax;
		}
		public void Nudge(float moveVal, float moveSpeed, float deltaTime)
		{
			greenPip.anchorMin = new Vector2(greenPip.anchorMin.x + deltaTime * moveSpeed * moveVal, greenPip.anchorMin.y);
			greenPip.anchorMax = new Vector2(greenPip.anchorMin.x + size.x, greenPip.anchorMax.y);
			if (greenPip.anchorMin.x < 0.0075)
			{
				greenPip.anchorMin = new Vector2(0.0075f, greenPip.anchorMin.y);
				greenPip.anchorMax = new Vector2(greenPip.anchorMin.x + size.x, greenPip.anchorMax.y);
			}
			if (greenPip.anchorMax.x > 0.9925)
			{
				greenPip.anchorMax = new Vector2(0.9925f, greenPip.anchorMax.y);
				greenPip.anchorMin = new Vector2(greenPip.anchorMax.x - size.x, greenPip.anchorMin.y);
			}
		}
		public void Move(float deltaTime, float currentSpeed, int moveDir)
		{
			greenPip.anchorMin = new Vector2(greenPip.anchorMin.x + deltaTime * currentSpeed * moveDir, greenPip.anchorMin.y);
			greenPip.anchorMax = new Vector2(greenPip.anchorMin.x + size.x, greenPip.anchorMax.y);
			if (greenPip.anchorMin.x < 0.0075)
			{
				greenPip.anchorMin = new Vector2(0.0075f, greenPip.anchorMin.y);
				greenPip.anchorMax = new Vector2(greenPip.anchorMin.x + size.x, greenPip.anchorMax.y);
			}
			if (greenPip.anchorMax.x > 0.9925)
			{
				greenPip.anchorMax = new Vector2(0.9925f, greenPip.anchorMax.y);
				greenPip.anchorMin = new Vector2(greenPip.anchorMax.x - size.x, greenPip.anchorMin.y);
			}
		}
		public bool CheckBalance(float left, float right)
		{
			if (greenPip.anchorMin.x <= left || greenPip.anchorMax.x >= right) return true;
			else return false;
		}
		public float GetParameterValue()
		{
			return Mathf.Abs(((greenPip.anchorMin.x + greenPip.anchorMax.x) / 2) - 0.5f) * 4 + 0.2f;
		}
		public float GetDistanceFromMiddle()
		{
			return Mathf.Abs(((greenPip.anchorMin.x + greenPip.anchorMax.x) / 2) - 0.5f) * 2;
		}
	}
	[SerializeField] private RectTransform greenPip = null;
	[SerializeField] private RectTransform redZone1 = null;
	[SerializeField] private RectTransform redZone2 = null;
	[SerializeField] private RectTransform leftArrow = null;
	[SerializeField] private RectTransform rightArrow = null;
	[SerializeField] private float nudgeSpeed = 5f;
	[SerializeField] private float fallLimit = 1f;
	[SerializeField] private float fallSpeed = 1f;
	[SerializeField] private float fallSpeedMultiplier = 1f;
	[SerializeField] private UnityEvent fallEvents = null;
	private Vector2 moveVal = Vector2.zero;
	private float offBalance = 0f;
	private float currentSpeed = 0;
	private int moveDir = 0;
	private GreenZone greenZone;
	[SerializeField] [EventRef] protected string plankBalancingSound = null;
	private string plankShakeParameter = "ShakeLevel";
	private EventInstance balancingSoundInstance;
	public void OnInput(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid() || gameObject.activeSelf == false) return;
		moveVal = value.ReadValue<Vector2>();
		if (moveVal.x > 0 && moveDir > 0) moveVal.x = 0;
		if (moveVal.x < 0 && moveDir < 0) moveVal.x = 0;
		greenZone.Nudge(moveVal.x, nudgeSpeed, Time.deltaTime);
	}
	public void ResetGame()
	{
		balancingSoundInstance.start();
		offBalance = 0f;
		if (greenZone == null) greenZone = new GreenZone(ref greenPip);
		greenZone.Reset();
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
		float greenPos = (greenPip.anchorMax.x + greenPip.anchorMin.x) * 0.5f;
		if (greenPos > 0.5f)
		{
			moveDir = 1;
			leftArrow.gameObject.SetActive(true);
			rightArrow.gameObject.SetActive(false);
		}
		else
		{
			moveDir = -1;
			leftArrow.gameObject.SetActive(false);
			rightArrow.gameObject.SetActive(true);
		}
		currentSpeed = greenZone.GetDistanceFromMiddle() * fallSpeedMultiplier + fallSpeed;
		greenZone.Move(Time.deltaTime, currentSpeed, moveDir);
		if (greenZone.CheckBalance(redZone1.anchorMax.x, redZone2.anchorMin.x))
		{
			offBalance += Time.deltaTime;
		}
		else if (offBalance > 0f)
		{
			offBalance -= Time.deltaTime;
		}
		float parameterValue = greenZone.GetParameterValue();
		//Debug.Log(parameterValue);
		balancingSoundInstance.setParameterByName(plankShakeParameter, parameterValue);
		balancingSoundInstance.getParameterByName(plankShakeParameter, out parameterValue);
		if (offBalance > fallLimit)
		{
			balancingSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			fallEvents.Invoke();
			ResetGame();
		}
	}
}
