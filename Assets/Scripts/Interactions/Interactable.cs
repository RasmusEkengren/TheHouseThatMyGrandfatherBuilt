using UnityEngine;

public class Interactable : MonoBehaviour
{
	private Camera mainCamera = null;
	[SerializeField] protected GameObject interactIcon = null;
	[SerializeField] private string playerTag = "Player";
	void Start()
	{
		mainCamera = Camera.main;
		interactIcon.transform.forward = mainCamera.transform.forward;
	}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == playerTag)
		{
			interactIcon.SetActive(true);
		}
	}
	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == playerTag)
		{
			interactIcon.SetActive(false);
		}
	}
	public virtual void Interact(GameObject player)
	{
		interactIcon.SetActive(false);
	}
}