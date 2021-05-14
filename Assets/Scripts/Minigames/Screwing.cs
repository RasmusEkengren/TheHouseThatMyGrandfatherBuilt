using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Screwing : MonoBehaviour
{
	[Serializable]
	private class Screw
	{
		[SerializeField] public RectTransform screwRect;
		[SerializeField] public GameObject[] buttonPrompts = new GameObject[4];
		[SerializeField] public GameObject glow;
		[SerializeField] public float startSize;
		[SerializeField] public float finishSize;
		public Screw(RectTransform screwRect, GameObject[] buttonPrompts, GameObject glow)
		{
			this.screwRect = screwRect;
			this.glow = glow;
			this.buttonPrompts = buttonPrompts;
		}
		public void Start()
		{
			screwRect.localScale = new Vector3(startSize, startSize, 1);
		}
		public void setAngle(float angle)
		{
			screwRect.eulerAngles = new Vector3(0, 0, angle);
			if (angle <= 337.5 && angle >= 202.5) buttonPrompts[0].SetActive(true);
			else buttonPrompts[0].SetActive(false);
			if (angle <= 247.5 && angle >= 112.5) buttonPrompts[1].SetActive(true);
			else buttonPrompts[1].SetActive(false);
			if (angle <= 157.5 && angle >= 22.5) buttonPrompts[2].SetActive(true);
			else buttonPrompts[2].SetActive(false);
			if (angle <= 67.5 || angle >= 292.5) buttonPrompts[3].SetActive(true);
			else buttonPrompts[3].SetActive(false);
		}
		public void Move(float t)
		{
			float scale = Mathf.Lerp(startSize, finishSize, t);
			screwRect.localScale = new Vector3(scale, scale, 1);
		}
		public void Select()
		{
			screwRect.gameObject.SetActive(true);
			glow.SetActive(true);
		}
		public void DeSelect()
		{
			glow.SetActive(false);
			foreach (GameObject buttonPrompt in buttonPrompts)
			{
				buttonPrompt.SetActive(false);
			}
		}
	}
	[SerializeField] private float screwTime = 3;
	[SerializeField] private float angleTolerance = 40;
	[SerializeField] private float screwingSpeed = 720;
	[SerializeField] private Screw[] screws = new Screw[4];
	[SerializeField] private int numberOfGameCompletions = 1;
	[SerializeField] private UnityEvent endEvent = null;
	[SerializeField] [FMODUnity.EventRef] protected string screwSound = null;
	[SerializeField] private float soundLength = 0.5f;
	private static int gameCompletions = 0;
	private int currentScrew = 0;
	private float moveAngle = 0f;
	private float targetAngle = 90f;
	private float timer = 0;
	private float soundTimer = 0;
	public void Move(Vector2 val)
	{
		moveAngle = Mathf.Atan2(val.y, val.x) * Mathf.Rad2Deg + 180;
	}
	void Start()
	{
		foreach (Screw screw in screws)
		{
			screw.Start();
		}
		currentScrew = 0;
		targetAngle = UnityEngine.Random.Range(0, 360);
		screws[currentScrew].Select();
		screws[currentScrew].setAngle(targetAngle);
	}
	void Update()
	{
		if (currentScrew < screws.Length)
		{
			if (soundTimer < soundLength) soundTimer += Time.deltaTime;
			if ((moveAngle < targetAngle + angleTolerance && moveAngle > targetAngle - angleTolerance) ||
				 moveAngle < targetAngle + 360 + angleTolerance && moveAngle > targetAngle + 360 - angleTolerance)
			{
				targetAngle -= screwingSpeed * Time.deltaTime;
				timer += Time.deltaTime;
				screws[currentScrew].Move(timer / screwTime);
				if (soundTimer >= soundLength)
				{
					FMODUnity.RuntimeManager.PlayOneShot(screwSound);
					soundTimer = 0;
				}
				if (targetAngle <= 0) targetAngle = 360;
				screws[currentScrew].setAngle(targetAngle);
			}
			if (timer >= screwTime)
			{
				screws[currentScrew].DeSelect();
				currentScrew++;
				if (currentScrew >= screws.Length)
				{
					gameCompletions++;
					if (gameCompletions >= numberOfGameCompletions)
					{
						GlobalSceneData.mg_windowsFixed = true;
						//Change this once we have sawing game done
						GlobalSceneData.leahState = GlobalSceneData.LeahState.Done;
						endEvent.Invoke();
					}
					return;
				}
				targetAngle = UnityEngine.Random.Range(0, 360);
				screws[currentScrew].Select();
				screws[currentScrew].setAngle(targetAngle);
				timer = 0;
			}
		}
	}
}
