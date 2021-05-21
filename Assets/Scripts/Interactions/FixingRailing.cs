using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixingRailing : MonoBehaviour
{
	[SerializeField] private InkManager playerInkManager;
	[SerializeField] private TextAsset[] FlatTopStories;
	[SerializeField] private TextAsset[] PillarsStories;
	[SerializeField] private UnityEvent lastPartEvent;
	private GameObject partToActivate = null;
	private int currentPillar = 0;
	public void NextPillar()
	{
		if (currentPillar >= 1)
		{
			if (GlobalSceneData.railingStyle == GlobalSceneData.RailingStyle.FlatTop)
			{
				playerInkManager.StartStory(FlatTopStories[currentPillar]);
				playerInkManager.DisplayNextLine();
			}
			else if (GlobalSceneData.railingStyle == GlobalSceneData.RailingStyle.Pillars)
			{
				playerInkManager.StartStory(PillarsStories[currentPillar]);
				playerInkManager.DisplayNextLine();
			}
			partToActivate.SetActive(true);
			lastPartEvent.Invoke();
		}
		else
		{
			if (GlobalSceneData.railingStyle == GlobalSceneData.RailingStyle.FlatTop)
			{
				playerInkManager.StartStory(FlatTopStories[currentPillar]);
				playerInkManager.DisplayNextLine();
			}
			else if (GlobalSceneData.railingStyle == GlobalSceneData.RailingStyle.Pillars)
			{
				playerInkManager.StartStory(PillarsStories[currentPillar]);
				playerInkManager.DisplayNextLine();
			}
			partToActivate.SetActive(true);
			currentPillar++;
		}
	}
	public void SetNextPillar(GameObject pillar)
	{
		partToActivate = pillar;
	}
}
