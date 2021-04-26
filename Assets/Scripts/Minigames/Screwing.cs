using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
[Serializable]
public class Screw
{
	[SerializeField] public RectTransform screwPos;
	[SerializeField] public GameObject glow;
	public Screw(RectTransform screwPos, GameObject glow)
	{
		this.screwPos = screwPos;
		this.glow = glow;
	}
	public void setAngle(float angle)
	{
		screwPos.eulerAngles = new Vector3(0, 0, angle);
	}
	public void Select()
	{
		glow.SetActive(true);
	}
	public void DeSelect()
	{
		glow.SetActive(false);
	}
}
public class Screwing : MonoBehaviour
{
	[SerializeField] private float screwTime = 3;
	[SerializeField] private float angleTolerance = 40;
	[SerializeField] private float screwingSpeed = 720;
	[SerializeField] private float arrowOffsetAngle = 45;
	[SerializeField] private Screw[] screws = new Screw[4];
	[SerializeField] private UnityEvent endEvent = null;
	private int currentScrew = 0;
	private float moveAngle = 0f;
	private float targetAngle = 90f;
	private float timer = 0;
	public void OnMove(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		Vector2 val = value.ReadValue<Vector2>();
		moveAngle = Mathf.Atan2(val.y, val.x) * Mathf.Rad2Deg + 180;
	}
	void Start()
	{
		currentScrew = 0;
		screws[currentScrew].Select();
	}
	// Update is called once per frame
	void Update()
	{
		if (currentScrew < screws.Length)
		{
			if ((moveAngle < targetAngle + angleTolerance && moveAngle > targetAngle - angleTolerance) ||
				 moveAngle < targetAngle + 360 + angleTolerance && moveAngle > targetAngle + 360 - angleTolerance)
			{
				targetAngle -= screwingSpeed * Time.deltaTime;
				timer += Time.deltaTime;
				if (targetAngle <= 0) targetAngle = 360;
				screws[currentScrew].setAngle(targetAngle + arrowOffsetAngle);
			}
			if (timer >= screwTime)
			{
				screws[currentScrew].DeSelect();
				currentScrew++;
				if (currentScrew >= screws.Length)
				{
					endEvent.Invoke();
					return;
				}
				screws[currentScrew].Select();
				timer = 0;
			}

		}
	}
}
