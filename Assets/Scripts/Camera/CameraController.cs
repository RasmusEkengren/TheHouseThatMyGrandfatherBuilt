using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Vector3 standardCameraOffset = new Vector3(-4, 7, 4);
	public Vector3 standardRotation;
	[SerializeField] private GameObject player;
	[SerializeField] private float smoothTime;
	[SerializeField] private float smoothLength;
	[SerializeField] private float rotationSmoothTime;
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotationVelocity = Vector3.zero;
	private Vector3 targetRotation;
	private Vector3 targetPosition;
	private Vector3 currentCameraOffset;
	private float currentSmoothTime;
	private float currentRotationSmoothTime;
	private InkManager inkManager;
	private bool isFollowingPlayer;
	void Start()
	{
		inkManager = player.GetComponent<InkManager>();
		targetRotation = transform.rotation.eulerAngles;
		if (GlobalSceneData.leahState != GlobalSceneData.LeahState.Entering || SceneManager.GetActiveScene().name == "George" || SceneManager.GetActiveScene().name == "Inside")
		{
			ResetRotation();
			currentCameraOffset = standardCameraOffset;
			currentSmoothTime = smoothTime;
		}
		isFollowingPlayer = true;
	}
	void LateUpdate()
	{
		if (isFollowingPlayer && !inkManager.isCutsceneActive)
		{
			targetPosition = player.transform.position + currentCameraOffset;
			if (Vector3.Distance(transform.position, targetPosition) > smoothLength)
			{
				Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, currentSmoothTime);
				transform.position = smoothPosition;
			}
		}
		else
		{
			if (currentSmoothTime > smoothTime) currentSmoothTime -= Time.deltaTime;
			if (currentSmoothTime < smoothTime) currentSmoothTime = smoothTime;
			Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, currentSmoothTime);
			transform.position = smoothPosition;
		}
		if (Vector3.Distance(transform.rotation.eulerAngles, targetRotation) > 0.01)
		{
			if (currentRotationSmoothTime > rotationSmoothTime) currentRotationSmoothTime -= Time.deltaTime;
			if (currentRotationSmoothTime < rotationSmoothTime) currentRotationSmoothTime = rotationSmoothTime;
			Vector3 smoothrotation = Vector3.SmoothDamp(transform.rotation.eulerAngles, targetRotation, ref rotationVelocity, currentRotationSmoothTime);
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
	public void SetTargetPosition(Vector3 position)
	{
		isFollowingPlayer = false;
		targetPosition = position;
	}
	public void SetTransform(Transform transform)
	{
		isFollowingPlayer = false;
		targetPosition = transform.position;
		targetRotation = transform.rotation.eulerAngles;
	}
	public void ResetPosition()
	{
		isFollowingPlayer = true;
	}
	public void SetPositionSmoothTime(float value)
	{
		currentSmoothTime = value;
	}
	public void SetRotationSmoothTime(float value)
	{
		rotationSmoothTime = value;
	}
	public void SetFollowOffset(Transform offset)
	{
		currentCameraOffset = offset.position;
	}
	public void SetRotation(Transform transform)
	{
		targetRotation = transform.rotation.eulerAngles;
	}
	public void ResetFollowOffset()
	{
		currentCameraOffset = standardCameraOffset;
	}
	public void TimedResetTransform(float t)
	{
		StartCoroutine(TimedReset(t));
	}
	public Vector3 GetStandardCameraOffset()
	{
		return standardCameraOffset;
	}
	private IEnumerator TimedReset(float time)
	{
		Vector3 startOffset = currentCameraOffset;
		Vector3 finishOffset = standardCameraOffset;
		Vector3 startRotation = targetRotation;
		Vector3 finishRotation = standardRotation;
		float t = 0;
		while (t <= time)
		{
			currentCameraOffset = Vector3.Lerp(startOffset, finishOffset, t / time);
			targetRotation = Vector3.Lerp(startRotation, finishRotation, t / time);
			t += Time.deltaTime;
			yield return null;
		}
	}
}
