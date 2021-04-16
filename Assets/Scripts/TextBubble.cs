using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
	private Camera mainCamera;

	void Start()
	{
		mainCamera = Camera.main;
	}

	void LateUpdate()
	{
		transform.forward = mainCamera.transform.forward;
	}
}
