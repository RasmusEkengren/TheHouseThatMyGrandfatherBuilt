using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartCutscene : MonoBehaviour
{
	[SerializeField] private UnityEvent startEvents;
	void Start()
	{
		startEvents.Invoke();
	}
}
