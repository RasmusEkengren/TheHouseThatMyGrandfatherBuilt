using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : Interactable
{
	[SerializeField] private bool isOneTime = false;
	[SerializeField] private UnityEvent interactions = null;
	private bool isTriggered = false;
	public override void Interact(GameObject player)
	{
		if (!isTriggered)
		{
			interactions.Invoke();
			interactIcon.SetActive(false);
			isTriggered = isOneTime;
		}
	}
}
