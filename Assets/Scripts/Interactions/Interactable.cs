using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Interactable : MonoBehaviour
{
	private Camera mainCamera;
	[SerializeField] private GameObject interactIcon;
	[SerializeField] private string playerTag;
	[SerializeField] private TextAsset StoryJson;
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
	public void Interact(GameObject player)
	{
		player.SendMessage("StartStory", StoryJson, SendMessageOptions.DontRequireReceiver);
	}
}