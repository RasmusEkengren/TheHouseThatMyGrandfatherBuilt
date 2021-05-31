using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Screwing : MonoBehaviour
{
	[Serializable]
	private class Screw
	{
		[SerializeField] public RectTransform screwRect;
		[SerializeField] public GameObject[] buttonPrompts = new GameObject[4];
		[SerializeField] public Animator[] buttonPromptAnimators = new Animator[4];
		[SerializeField] public GameObject[] buttonGlows = new GameObject[4];
		[SerializeField] public GameObject glow;
		[SerializeField] public float startSize;
		[SerializeField] public float finishSize;
		public Screw(RectTransform screwRect, GameObject[] buttonPrompts, Animator[] buttonPromptAnimators, GameObject[] buttonGlows, GameObject glow)
		{
			this.screwRect = screwRect;
			this.glow = glow;
			this.buttonPrompts = buttonPrompts;
			this.buttonGlows = buttonGlows;
			this.buttonPromptAnimators = buttonPromptAnimators;
		}
		public void Start()
		{
			screwRect.localScale = new Vector3(startSize, startSize, 1);
		}
		public void setAngle(float angle)
		{
			screwRect.eulerAngles = new Vector3(0, 0, angle);
			if (angle <= 337.5 && angle >= 202.5) buttonGlows[0].SetActive(true);
			else buttonGlows[0].SetActive(false);
			if (angle <= 247.5 && angle >= 112.5) buttonGlows[1].SetActive(true);
			else buttonGlows[1].SetActive(false);
			if (angle <= 157.5 && angle >= 22.5) buttonGlows[2].SetActive(true);
			else buttonGlows[2].SetActive(false);
			if (angle <= 67.5 || angle >= 292.5) buttonGlows[3].SetActive(true);
			else buttonGlows[3].SetActive(false);
		}
		public void Move(float t)
		{
			float scale = Mathf.Lerp(startSize, finishSize, t);
			screwRect.localScale = new Vector3(scale, scale, 1);
		}
		public void AnimateButtonPrompts(Vector2 val)
		{
			if (val.x < -0.1) buttonPromptAnimators[3].SetBool("Pressed", true);
			else buttonPromptAnimators[3].SetBool("Pressed", false);
			if (val.x > 0.1) buttonPromptAnimators[1].SetBool("Pressed", true);
			else buttonPromptAnimators[1].SetBool("Pressed", false);
			if (val.y < -0.1) buttonPromptAnimators[2].SetBool("Pressed", true);
			else buttonPromptAnimators[2].SetBool("Pressed", false);
			if (val.y > 0.1) buttonPromptAnimators[0].SetBool("Pressed", true);
			else buttonPromptAnimators[0].SetBool("Pressed", false);
		}
		public void Select()
		{
			screwRect.gameObject.SetActive(true);
			glow.SetActive(true);
			foreach (GameObject buttonPrompt in buttonPrompts)
			{
				buttonPrompt.SetActive(true);
			}
		}
		public void DeSelect()
		{
			glow.SetActive(false);
			foreach (GameObject buttonPrompt in buttonPrompts)
			{
				buttonPrompt.SetActive(false);
			}
			foreach (GameObject buttonGlow in buttonGlows)
			{
				buttonGlow.SetActive(false);
			}
		}
	}
	[SerializeField] private float screwTime = 3;
	[SerializeField] private float angleTolerance = 40;
	[SerializeField] private float screwingSpeed = 720;
	[SerializeField] private Screw[] screws = new Screw[4];
	[SerializeField] [FMODUnity.EventRef] protected string screwSound = null;
	[SerializeField] private float soundLength = 0.5f;
	private int currentScrew = 0;
	private float moveAngle = 0f;
	private float targetAngle = 90f;
	private float timer = 0;
	private float soundTimer = 0;
	public void Move(Vector2 val)
	{
		moveAngle = Mathf.Atan2(val.y, val.x) * Mathf.Rad2Deg + 180;
		if (currentScrew < screws.Length)
			screws[currentScrew].AnimateButtonPrompts(val);
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
					GetComponentInParent<ScrewParent>().NextGame();
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
