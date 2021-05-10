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
		[SerializeField] public RectTransform screwHeadRect;
		[SerializeField] public RectTransform holeRect;

		[SerializeField] public RectTransform buttonPromptsRect;
		[SerializeField] public GameObject[] buttonPrompts = new GameObject[4];
		[SerializeField] public GameObject glow;
		private Vector2 headSize;
		private Vector2 screwSize;
		private Vector2 buttonPromptsSize;
		private Vector2 startPos;
		private Vector2 finishPos;
		public Screw(RectTransform screwRect, RectTransform screwHeadRect, RectTransform holeRect, RectTransform buttonPromptsRect, GameObject[] buttonPrompts, GameObject glow)
		{
			this.screwRect = screwRect;
			this.screwHeadRect = screwHeadRect;
			this.holeRect = holeRect;
			this.glow = glow;
			this.buttonPromptsRect = buttonPromptsRect;
			this.buttonPrompts = buttonPrompts;
		}
		private void setHeadPos()
		{
			Vector2 headTargetPos = new Vector2(screwRect.anchorMin.x + (screwRect.pivot.x * screwSize.x), screwRect.anchorMin.y + (screwRect.pivot.y * screwSize.y));
			screwHeadRect.anchorMin = new Vector2(headTargetPos.x - headSize.x * 0.5f, headTargetPos.y - headSize.y * 0.5f);
			screwHeadRect.anchorMax = new Vector2(headTargetPos.x + headSize.x * 0.5f, headTargetPos.y + headSize.y * 0.5f);
			buttonPromptsRect.anchorMin = new Vector2(screwHeadRect.anchorMin.x - buttonPromptsSize.x * 0.5f, screwHeadRect.anchorMin.y - buttonPromptsSize.y * 0.5f);
			buttonPromptsRect.anchorMax = new Vector2(screwHeadRect.anchorMax.x + buttonPromptsSize.x * 0.5f, screwHeadRect.anchorMax.y + buttonPromptsSize.y * 0.5f);
		}
		public void Start()
		{
			headSize = new Vector2(screwHeadRect.anchorMax.x - screwHeadRect.anchorMin.x, screwHeadRect.anchorMax.y - screwHeadRect.anchorMin.y);
			screwSize = new Vector2(screwRect.anchorMax.x - screwRect.anchorMin.x, screwRect.anchorMax.y - screwRect.anchorMin.y);
			buttonPromptsSize = new Vector2(buttonPromptsRect.anchorMax.x - buttonPromptsRect.anchorMin.x, buttonPromptsRect.anchorMax.y - buttonPromptsRect.anchorMin.y);
			startPos = new Vector2(screwRect.anchorMin.x + (screwRect.pivot.x * screwSize.x), screwRect.anchorMin.y + (screwRect.pivot.y * screwSize.y));
			finishPos = ((holeRect.anchorMax - holeRect.anchorMin) * 0.5f) + holeRect.anchorMin;
		}
		public void setAngle(float angle)
		{
			setHeadPos();
			screwHeadRect.eulerAngles = new Vector3(0, 0, angle);
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
			screwRect.anchorMin = Vector2.Lerp(startPos, finishPos, t);
			screwRect.anchorMin = new Vector2(screwRect.anchorMin.x - (screwRect.pivot.x * screwSize.x), screwRect.anchorMin.y - (screwRect.pivot.y * screwSize.y));
			screwRect.anchorMax = screwRect.anchorMin + screwSize;
		}
		public void Select()
		{
			screwHeadRect.gameObject.SetActive(true);
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
					endEvent.Invoke();
					gameCompletions++;
					if (gameCompletions >= 1) GlobalSceneData.mg_windowsFixed = true;
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
