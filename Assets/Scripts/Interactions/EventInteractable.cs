using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : Interactable
{
	[SerializeField] private bool isOneTime = false;
	[SerializeField] private UnityEvent interactions = null;
	private bool isTriggered = false;
	public override void Interact(GameObject player)
	{
		if (!isTriggered && interactIcon.activeSelf)
		{
			interactIcon.SetActive(false);
			interactions.Invoke();
			FMODUnity.RuntimeManager.PlayOneShot(interactSound);
			isTriggered = isOneTime;
		}
	}
	public void setIsTrigger(bool triggered)
	{
		isTriggered = triggered;
	}
}
