using UnityEngine;
using UnityEngine.Events;

public class AddInkManagerEvent : MonoBehaviour
{
	[SerializeField] private UnityEvent _event;
	[SerializeField] private InkManager inkManager;
	public void AddEvent()
	{
		inkManager.SetEndEvent(_event);
	}
}
