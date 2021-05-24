using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnStart : MonoBehaviour
{
	[SerializeField] private UnityEvent startEvents;
	[SerializeField] private bool isTriggeredOnce = false;
	void Start()
	{
		if (isTriggeredOnce)
		{
			this.gameObject.GetComponent<UniqueID>().CheckID();
			if (GlobalSceneData.FindInteractedState(this.gameObject.GetComponent<UniqueID>().ID))
			{
				return;
			}
			else
			{
				startEvents.Invoke();
				GlobalSceneData.interactedObjectIDs.Add(this.gameObject.GetComponent<UniqueID>().ID);
			}
		}
		else
		{
			startEvents.Invoke();
		}
	}
}
