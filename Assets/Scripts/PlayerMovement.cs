using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController playerController;
	public float moveSpeed = 6f;
	public float turnSmoothTime = 0.1f;

	private Vector3 direction;
	private float turnSmoothVelocity;

	public void OnMove(InputValue value)
	{
		Vector2 moveVal = value.Get<Vector2>();
		direction = new Vector3(moveVal.x,0f,moveVal.y);
	}

    void Update()
    {
		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			float rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

			transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

			playerController.Move(direction * moveSpeed * Time.deltaTime);
		}
	}
}
