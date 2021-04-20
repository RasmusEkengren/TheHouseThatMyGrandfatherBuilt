using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{
	[SerializeField] private GameObject followObject = null;
	private Camera mainCamera = null;
	private Vector2 viewPortPos = Vector2.zero;
	private RectTransform rectTransform = null;
	void Start()
	{
		mainCamera = Camera.main;
		rectTransform = GetComponent<RectTransform>();
	}

	void LateUpdate()
	{
		viewPortPos = mainCamera.WorldToScreenPoint(followObject.transform.position);
		rectTransform.position = new Vector3(viewPortPos.x, viewPortPos.y, 0f);
	}
}
