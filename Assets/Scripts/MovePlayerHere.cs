using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerHere : MonoBehaviour
{
	private PlayerMovement player = null;
	private Camera cam = null;

	void Start()
	{
		player = FindObjectOfType<PlayerMovement>();
		cam = Camera.main;
		player.gameObject.transform.position = gameObject.transform.position;
		player.gameObject.transform.rotation = gameObject.transform.rotation;
		cam.gameObject.transform.position = player.transform.position + cam.GetComponent<CameraController>().GetStandardCameraOffset();
	}

	private void OnDrawGizmos()
	{
		Gizmos.matrix = this.transform.localToWorldMatrix;
		Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
		Gizmos.DrawCube(Vector3.zero, new Vector3(0.5f, 1, 0.5f));
		Gizmos.color = new Color(0.7f, 0f, 0f, 1f);
		Gizmos.DrawLine(Vector3.zero, Vector3.zero + Vector3.forward);
	}
}
