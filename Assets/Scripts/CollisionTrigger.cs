using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
	[SerializeField] private bool isOneTime = false;
	public UnityEvent eventsToTrigger = null;
	private bool isTriggered = false;
	private void OnTriggerEnter(Collider other)
	{
		if (!isTriggered)
		{
			isTriggered = isOneTime;
			Debug.Log("Collision trigger", gameObject);
			eventsToTrigger.Invoke();
		}
	}
}
