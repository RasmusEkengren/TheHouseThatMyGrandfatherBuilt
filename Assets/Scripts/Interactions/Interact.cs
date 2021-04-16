using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
	private Vector3 center;
	[SerializeField] private float interactRadius;
	public void OnInteract(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed)
		{
			center = transform.position;
			Collider[] hits = Physics.OverlapSphere(center, interactRadius);
			foreach (Collider hit in hits)
			{
				hit.SendMessage("Interact", this.gameObject, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
