using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Vector3 cameraOffset;
	public Vector3 standardRotation;
	[SerializeField] private GameObject player;
	[SerializeField] private float smoothTime;
	[SerializeField] private float smoothLength;
	[SerializeField] private float rotationSmoothTime;
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotationVelocity = Vector3.zero;
	private Vector3 targetRotation;
	private InkManager inkManager;
	void Start()
	{
		inkManager = player.GetComponent<InkManager>();
		targetRotation = transform.rotation.eulerAngles;
	}
	void LateUpdate()
	{
		if (inkManager.isCutsceneActive) return;
		Vector3 targetPosition = player.transform.position + cameraOffset;
		if (Vector3.Distance(transform.position, targetPosition) > smoothLength)
		{
			Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
			transform.position = smoothPosition;
		}
		if (Vector3.Distance(transform.rotation.eulerAngles, targetRotation) > 0.01)
		{
			Vector3 smoothrotation = Vector3.SmoothDamp(transform.rotation.eulerAngles, targetRotation, ref rotationVelocity, rotationSmoothTime);
			transform.eulerAngles = smoothrotation;
		}
	}
	public void SetRotation(Vector3 rotation)
	{
		targetRotation = rotation;
	}
	public void ResetRotation()
	{
		targetRotation = standardRotation;
	}
}
