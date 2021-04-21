using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : ConditionalInteraction
{
	[SerializeField] private AxeTracker player;
	public void CheckAxe()
	{
		CheckCondition(player.hasAxe);
	}
}
