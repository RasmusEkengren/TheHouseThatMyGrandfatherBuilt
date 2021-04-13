using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
	private Vector3 center;
	[SerializeField] private float interactRadius;
	void OnInteract()
	{
		center = transform.position;
		Collider[] hits = Physics.OverlapSphere(center, interactRadius);
		foreach (Collider hit in hits)
		{
			hit.SendMessage("Interact", this.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}
}
