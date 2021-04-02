using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController playerController;
	public Camera mainCamera;
	public float moveSpeed = 6f;
	public float turnSmoothTime = 0.1f;

	private Vector3 direction;
	private float turnSmoothVelocity;

	public void OnMove(InputValue value)
	{
		Vector2 moveVal = value.Get<Vector2>();
		direction = new Vector3(moveVal.x, 0f, moveVal.y);
	}

    void Update()
    {
		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
			float rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			playerController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
		}
	}
}
