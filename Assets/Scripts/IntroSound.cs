using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntroSound : MonoBehaviour
{
	[SerializeField] private UnityEvent IntroEndEvents = null;
	[SerializeField] private Image BlackPanel = null;
	[SerializeField] [EventRef] protected string IntroEvent = null;

	public void StartIntroEvent()
	{
		FMODUnity.RuntimeManager.PlayOneShot(IntroEvent);
		StartCoroutine(Timer(14f));
	}
	public void FadeOutPanel(float f)
	{
		StartCoroutine(Fade(f));
	}
	private IEnumerator Timer(float time)
	{
		float t = 0;
		while (t <= time)
		{
			t += Time.deltaTime;
			yield return null;
		}
		IntroEndEvents.Invoke();
	}
	private IEnumerator Fade(float t)
	{
		float startTime = t;
		while (t > 0)
		{
			BlackPanel.color = new Color(BlackPanel.color.r, BlackPanel.color.g, BlackPanel.color.r, t / startTime);
			t -= Time.deltaTime;
			yield return null;
		}
		BlackPanel.gameObject.SetActive(false);
	}
}
