using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : ConditionalInteraction
{
	[SerializeField] private AxeTracker player;
	[SerializeField] [FMODUnity.EventRef] string treeFall = null;
	public void CheckAxe()
	{
		CheckCondition(player.hasAxe);
	}

	public void Play()
	{
		FMODUnity.RuntimeManager.PlayOneShot(treeFall);
	}
}