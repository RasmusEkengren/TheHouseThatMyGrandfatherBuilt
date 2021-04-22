using UnityEngine;
using UnityEngine.Events;

public class EventInvoke : MonoBehaviour
{
	[SerializeField] private UnityEvent events;
	public void InvokeEvents()
	{
		events.Invoke();
	}
}
