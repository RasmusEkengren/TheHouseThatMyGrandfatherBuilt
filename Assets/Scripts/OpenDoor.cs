using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	[SerializeField] private Transform door;
	[SerializeField] private float swingTime = 0.75f;
	[SerializeField] private float openAngle = -75f;
	[SerializeField] private float closeAngle = 0f;
	private string playerTag = "Player";
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == playerTag)
		{
			StartCoroutine(Open());
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == playerTag)
		{
			StartCoroutine(Close());
		}
	}
	private IEnumerator Open()
	{
		float t = 0;
		while (t < 1)
		{
			t += Time.deltaTime * swingTime;
			door.localRotation = Quaternion.Euler(0, Mathf.LerpAngle(closeAngle, openAngle, t), 0);
			yield return null;
		}
	}
	private IEnumerator Close()
	{
		float t = 0;
		while (t < 1)
		{
			t += Time.deltaTime * swingTime;
			door.localRotation = Quaternion.Euler(0, Mathf.LerpAngle(openAngle, closeAngle, t), 0);
			yield return null;
		}
	}
}