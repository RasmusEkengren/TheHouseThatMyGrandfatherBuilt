using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.UI;

public class Sawing : MonoBehaviour
{
	[SerializeField] private RectTransform saw = null;
	[SerializeField] private RectTransform cutLine = null;
	[SerializeField] private RectTransform leftPiece = null;
	[SerializeField] private Image leftPieceImage = null;
	[SerializeField] private RectTransform rightPiece = null;
	[SerializeField] private Image rightPieceImage = null;
	[SerializeField] private RectTransform wholePlank = null;
	[SerializeField] private GameObject plankCounter = null;
	private Animator sawAnimator = null;
	private bool isCutting = false;
	private bool isFalling = false;
	private Vector2 moveVal = Vector2.zero;
	private float timer = 0;
	private int gameCompletions = 0;
	[SerializeField] private float sawMoveSpeed = 0.5f;
	[SerializeField] private float sawSlowMoveSpeed = 0.25f;
	[SerializeField] private float sawSlowTolerance = 0.05f;
	[SerializeField] private float sawCuttingSpeed = 1f;
	[SerializeField] private float lineTolerance = 0.02f;
	[SerializeField] private int sawCutNumber = 3;
	[SerializeField] private int numberOfPlanks = 6;
	[SerializeField] private float fallSpeed = 0.3f;
	[SerializeField] private float rotationSpeed = 1f;
	[SerializeField] private float fallTime = 1f;
	[SerializeField] [EventRef] protected string startCutSound = null;
	[SerializeField] [EventRef] protected string CuttingSound = null;
	[SerializeField] [EventRef] protected string PlankFallSound = null;
	[SerializeField] [EventRef] protected string FailCutSound = null;
	[SerializeField] private UnityEvent gameCompleteEvent = null;
	private EventInstance cuttingSoundInstance;
	private bool hasMoved = false;

	public int GetPlankCompletions() { return gameCompletions; }
	public int GetPlanksNumberToCut() { return numberOfPlanks; }
	public void ResetGame()
	{
		gameCompletions = 0;
		timer = 0;
		isCutting = false;
		isFalling = false;
		StartGame();
	}
	public void StartGame()
	{
		leftPiece.gameObject.SetActive(false);
		rightPiece.gameObject.SetActive(false);
		wholePlank.gameObject.SetActive(true);
		cutLine.gameObject.SetActive(true);
		saw.gameObject.SetActive(true);
		plankCounter.SetActive(true);
		float cutlinePos = Random.Range(0.1f, 0.9f);
		cutLine.anchorMin = new Vector2(cutlinePos - 0.0125f, cutLine.anchorMin.y);
		cutLine.anchorMax = new Vector2(cutlinePos + 0.0125f, cutLine.anchorMax.y);
		leftPiece.anchorMin = new Vector2(0, 0);
		leftPiece.anchorMax = new Vector2(1, 1);
		leftPieceImage.fillAmount = cutlinePos;
		leftPiece.rotation = Quaternion.identity;
		rightPiece.anchorMin = new Vector2(0, 0);
		rightPiece.anchorMax = new Vector2(1, 1);
		rightPieceImage.fillAmount = 1.0f - cutlinePos;
		rightPiece.rotation = Quaternion.identity;
		hasMoved = false;
	}
	public void StartCut(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed && !isCutting && !isFalling)
		{
			if (saw.anchorMax.x < cutLine.anchorMax.x + lineTolerance && saw.anchorMin.x > cutLine.anchorMin.x - lineTolerance)
			{
				isCutting = true;
				sawAnimator.SetTrigger("Saw");
				timer = 0;
			}
			else if (saw.gameObject.activeSelf && hasMoved)
			{
				FMODUnity.RuntimeManager.PlayOneShot(FailCutSound);
			}
		}
	}
	public void moveSaw(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		moveVal = value.ReadValue<Vector2>();
		if (saw.gameObject.activeSelf)
		{
			hasMoved = true;
		}
	}
	void OnEnable()
	{
		cuttingSoundInstance = RuntimeManager.CreateInstance(CuttingSound);
		sawAnimator = saw.GetComponent<Animator>();
	}
	void OnDisable()
	{
		cuttingSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		cuttingSoundInstance.release();
	}
	public void PlaySawCutSound()
	{
		cuttingSoundInstance.start();
	}
	public void PlayPlankFallSound()
	{
		cuttingSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		FMODUnity.RuntimeManager.PlayOneShot(PlankFallSound);
	}
	void Update()
	{
		if (isCutting)
		{
			timer += Time.deltaTime;
			if (timer >= sawAnimator.runtimeAnimatorController.animationClips[0].length)
			{
				isCutting = false;
				isFalling = true;
				leftPiece.gameObject.SetActive(true);
				rightPiece.gameObject.SetActive(true);
				wholePlank.gameObject.SetActive(false);
				cutLine.gameObject.SetActive(false);
				timer = 0;
			}
		}
		else if (isFalling)
		{
			timer += Time.deltaTime;
			leftPiece.anchorMin = new Vector2(leftPiece.anchorMin.x - fallSpeed * Time.deltaTime, leftPiece.anchorMin.y - fallSpeed * Time.deltaTime * 8);
			leftPiece.anchorMax = new Vector2(leftPiece.anchorMax.x - fallSpeed * Time.deltaTime, leftPiece.anchorMax.y - fallSpeed * Time.deltaTime * 8);
			leftPiece.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
			rightPiece.anchorMin = new Vector2(rightPiece.anchorMin.x + fallSpeed * Time.deltaTime, rightPiece.anchorMin.y - fallSpeed * Time.deltaTime * 8);
			rightPiece.anchorMax = new Vector2(rightPiece.anchorMax.x + fallSpeed * Time.deltaTime, rightPiece.anchorMax.y - fallSpeed * Time.deltaTime * 8);
			rightPiece.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
			if (timer >= fallTime)
			{
				isFalling = false;
				gameCompletions++;
				if (gameCompletions >= numberOfPlanks)
				{
					gameCompleteEvent.Invoke();
					return;
				}
				timer = 0;
				StartGame();
			}
		}
		else
		{
			if (saw.anchorMin.x > cutLine.anchorMin.x - sawSlowTolerance && saw.anchorMax.x < cutLine.anchorMax.x + sawSlowTolerance)
			{
				saw.anchorMin = new Vector2(saw.anchorMin.x + (moveVal.x * sawSlowMoveSpeed * Time.deltaTime), saw.anchorMin.y);
				saw.anchorMax = new Vector2(saw.anchorMax.x + (moveVal.x * sawSlowMoveSpeed * Time.deltaTime), saw.anchorMax.y);
			}
			else
			{
				saw.anchorMin = new Vector2(saw.anchorMin.x + (moveVal.x * sawMoveSpeed * Time.deltaTime), saw.anchorMin.y);
				saw.anchorMax = new Vector2(saw.anchorMax.x + (moveVal.x * sawMoveSpeed * Time.deltaTime), saw.anchorMax.y);
			}
		}
	}
}