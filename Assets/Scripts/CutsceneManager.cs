using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
	[SerializeField] private float borderSpeed;
	[SerializeField] private RectTransform topBorder;
	[SerializeField] private RectTransform bottomBorder;
	void Start()
	{
		if (GlobalSceneData.leahState != GlobalSceneData.LeahState.Entering)
		{
			EndBorder();
		}
	}
	public void StartBorder()
	{
		StartCoroutine(MoveBorder(true));
	}
	public void EndBorder()
	{
		StartCoroutine(MoveBorder(false));
	}
	private IEnumerator MoveBorder(bool isGoingIn)
	{
		float topTargetValue = isGoingIn ? 0.87037f : 1;
		float botTargetValue = isGoingIn ? 0.12963f : 0;
		float topStart = topBorder.anchorMin.y;
		float botStart = bottomBorder.anchorMax.y;
		float t = 0;
		while (t < 1)
		{
			topBorder.anchorMin = new Vector2(0, Mathf.Lerp(topStart, topTargetValue, t));
			bottomBorder.anchorMax = new Vector2(1, Mathf.Lerp(botStart, botTargetValue, t));
			t += Time.deltaTime * borderSpeed;
			yield return null;
		}
	}
}
