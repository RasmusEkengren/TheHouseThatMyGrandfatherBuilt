using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionalInteraction : MonoBehaviour
{
	[SerializeField] private UnityEvent failEvents;
	[SerializeField] private UnityEvent successEvents;
	public void CheckCondition(bool condition)
	{
		if (condition) successEvents.Invoke();
		else failEvents.Invoke();
	}
}
