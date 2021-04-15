using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : Interactable
{
	[SerializeField] private UnityEvent interactions = null;
	public override void Interact(GameObject player)
	{
		interactions.Invoke();
		interactIcon.SetActive(false);
	}
}
