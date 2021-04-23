using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using FMOD.Studio;
using FMODUnity;

public class Sawing : MonoBehaviour
{
	[SerializeField] private RectTransform saw = null;
	[SerializeField] private RectTransform plank = null;
	[SerializeField] private RectTransform cutLine = null;
	private bool isCutting = false;
	private Vector2 moveVal = Vector2.zero;
	[SerializeField] private float sawMoveSpeed = 200f;
	[SerializeField] private float lineTolerance = 6f;
	[SerializeField] [EventRef] protected string startCutSound = null;
	[SerializeField] [EventRef] protected string CuttingSound = null;
	private EventInstance cuttingSoundInstance;
	public void StartCut(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed && !isCutting)
		{
			if (saw.localPosition.x < cutLine.localPosition.x + lineTolerance && saw.localPosition.x > cutLine.localPosition.x - lineTolerance)
			{
				FMODUnity.RuntimeManager.PlayOneShot(startCutSound);
				isCutting = true;
			}
		}
	}
	public void moveSaw(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		moveVal = value.ReadValue<Vector2>();
	}
	void OnEnable()
	{
		cuttingSoundInstance = RuntimeManager.CreateInstance(CuttingSound);
	}
	void OnDisable()
	{
		cuttingSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		cuttingSoundInstance.release();
	}
	void Update()
	{
		if (isCutting)
		{
			//Maybe use volume instead, or parameter might work better;
			if (Mathf.Abs(moveVal.y) < 0.1) cuttingSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			else cuttingSoundInstance.start();
		}
		saw.localPosition += new Vector3(isCutting ? 0 : moveVal.x, isCutting ? moveVal.y : 0, 0) * sawMoveSpeed * Time.deltaTime;
	}
}