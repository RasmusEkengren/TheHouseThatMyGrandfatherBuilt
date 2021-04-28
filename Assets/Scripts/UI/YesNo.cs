using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YesNo : MonoBehaviour
{
	private UnityEvent yesEvent;
	public void SetYesEvent(UnityEvent unityEvent)
	{
		yesEvent = unityEvent;
	}
	public void InvokeYesEvent()
	{
		yesEvent.Invoke();
	}
}