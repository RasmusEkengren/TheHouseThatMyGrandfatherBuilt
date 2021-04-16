using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Vector3 cameraOffset;
	[SerializeField] private GameObject player;
	[SerializeField] private float smoothTime;
	[SerializeField] private float smoothLength;

	private Vector3 velocity = Vector3.zero;
	void LateUpdate()
	{
		Vector3 targetPosition = player.transform.position + cameraOffset;
		if (Vector3.Distance(transform.position, targetPosition) > smoothLength)
		{
			Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
			transform.position = smoothPosition;
		}
	}
}
