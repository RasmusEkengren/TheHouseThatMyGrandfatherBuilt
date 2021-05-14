using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private CharacterController playerController = null;
	[SerializeField] private float moveSpeed = 6f;
	[SerializeField] private float autoWalkSpeed = 3f;
	[SerializeField] private float fallSpeed = 0.5f;
	[SerializeField] private float turnSmoothTime = 0.1f;
	private Camera mainCamera = null;
	private Transform[] points;
	private int currentPoint = 0;
	private Vector3 direction = Vector3.zero;
	private bool isAutoWalking = false;
	[HideInInspector] public bool isWalking = false; // Used for animation
	private bool hasFallen = false;
	private float turnSmoothVelocity = 0f;
	private float groundCastMaxDist = 1.08f;
	private float timer = 0f;
	[SerializeField] private float fallTime = 1f;
	private float savedWalkSpeed = 0f;
	[SerializeField] private LayerMask groundLayer;

	private bool _speedrun = false;
	[HideInInspector]

	private PlayerFootstepSound playerFootstep = null;

	public void ChangeSpeed(float _moveSpeed, float _autoMoveSpeed)
	{
		moveSpeed = _moveSpeed;
		autoWalkSpeed = _autoMoveSpeed;
	}

	public void Move(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (isAutoWalking) return;
		Vector2 moveVal = value.ReadValue<Vector2>();
		direction = new Vector3(moveVal.x, 0f, moveVal.y);
	}
	public void AutoWalk(Transform[] movePoints)
	{
		currentPoint = 0;
		points = movePoints;
		isAutoWalking = true;
	}
	public void NextPoint()
	{
		currentPoint++;
	}
	public void FinalPoint()
	{
		points = null;
		isAutoWalking = false;
	}
	public void FallDown()
	{
		hasFallen = true;
		autoWalkSpeed = 0f;
	}
	void Start()
	{
		mainCamera = Camera.main;
		direction = Vector2.zero;
		savedWalkSpeed = autoWalkSpeed;
		playerFootstep = GetComponentInChildren<PlayerFootstepSound>();
	}

	void Update()
	{
		if (hasFallen)
		{
			timer += Time.deltaTime;
			if (timer >= fallTime)
			{
				autoWalkSpeed = savedWalkSpeed;
				hasFallen = false;
				timer = 0f;
			}
		}
		else
		{
			if (isAutoWalking)
			{
				direction = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y - 90f, 0f) * (transform.position - points[currentPoint].position).normalized;
				//Debug.Log(direction);
			}
			if (direction.magnitude >= 0.1f && !GameController.GamePaused())
			{
				isWalking = true;

				float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
				float rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
				transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

				Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
				playerController.Move(moveDir.normalized * (isAutoWalking ? autoWalkSpeed : moveSpeed) * Time.deltaTime);
			}
			else { isWalking = false; }
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCastMaxDist);
		if (mainCamera != null && direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			Gizmos.DrawLine(transform.position, transform.position + moveDir * 20);
		}
	}
	void FixedUpdate()
	{
		bool isGroundHit = Physics.Raycast(transform.position, Vector3.down, groundCastMaxDist, groundLayer);
		if (!isGroundHit) playerController.Move(Vector3.down * fallSpeed * Time.deltaTime);
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.tag == "Grass" || hit.gameObject.tag == "Mud" || hit.gameObject.tag == "Wood")
		{
			playerFootstep.currentCollision = hit.gameObject;
		}
	}
}
