using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankPile : MonoBehaviour
{
	[SerializeField] private PlayerMovement player = null;
	[SerializeField] private AutoWalkPath pointsToMoveTo;
	[SerializeField] private GameObject[] planksToDeactivate;
	public void PickUp()
	{
		//player.AutoWalk(pointsToMoveTo.points);
		//foreach (GameObject plank in planksToDeactivate)
		//{
		//	plank.SetActive(false);
		//}
	}
}
