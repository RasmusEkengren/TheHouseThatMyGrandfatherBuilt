using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddYesEvent : MonoBehaviour
{
	[SerializeField] private YesNo yesNoObject;
	[SerializeField] private UnityEvent eventsToAdd;
	public void AddEvent()
	{
		yesNoObject.SetYesEvent(eventsToAdd);
	}
}
