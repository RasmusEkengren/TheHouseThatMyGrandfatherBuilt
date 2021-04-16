using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private CharacterController playerController = null;
	[SerializeField] private float moveSpeed = 6f;
	[SerializeField] private float turnSmoothTime = 0.1f;
	private Camera mainCamera = null;
	private Vector3 direction = Vector3.zero;
	private float turnSmoothVelocity = 0f;

    [SerializeField] [FMODUnity.EventRef] private string footstepSound = null;

    private Vector3 previousFootstep;
    [SerializeField] private int footstepInterval = 1;

	void Start()
	{
		mainCamera = Camera.main;
        previousFootstep = gameObject.GetComponent<Transform>().position;
	}
	public void Move(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		Vector2 moveVal = value.ReadValue<Vector2>();
		direction = new Vector3(moveVal.x, 0f, moveVal.y);
	}

    private void PlayFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShot(footstepSound);
    }

	void Update()
	{
		if (direction.magnitude >= 0.1f && !GameController.GamePaused())
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
			float rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			playerController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(previousFootstep, gameObject.transform.position) > footstepInterval)
            {
                PlayFootstep();
                previousFootstep = gameObject.transform.position;
            }
        }
	}
}
