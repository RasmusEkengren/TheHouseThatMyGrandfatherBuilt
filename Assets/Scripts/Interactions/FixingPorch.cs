using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixingPorch : MonoBehaviour
{
	[SerializeField] private InkManager playerInkManager;
	[SerializeField] private TextAsset[] FlatStories;
	[SerializeField] private TextAsset[] SlantedStories;
	[SerializeField] private UnityEvent thirdPillarEvent;
	private GameObject pillarToActivate = null;
	private int currentPillar = 0;
	public void NextPillar()
	{
		if (currentPillar >= 2)
		{
			pillarToActivate.SetActive(true);
			thirdPillarEvent.Invoke();
		}
		else
		{
			if (GlobalSceneData.porchStyle == GlobalSceneData.PorchStyle.Flat)
			{
				playerInkManager.StartStory(FlatStories[currentPillar]);
				playerInkManager.DisplayNextLine();
			}
			else if (GlobalSceneData.porchStyle == GlobalSceneData.PorchStyle.Slanted)
			{
				playerInkManager.StartStory(SlantedStories[currentPillar]);
				playerInkManager.DisplayNextLine();
			}
			pillarToActivate.SetActive(true);
			currentPillar++;
		}
	}
	public void SetNextPillar(GameObject pillar)
	{
		pillarToActivate = pillar;
	}
}
