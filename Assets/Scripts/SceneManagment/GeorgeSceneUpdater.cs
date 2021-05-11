using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeSceneUpdater : MonoBehaviour
{
	private void Start()
	{
		foreach (ObjectState objectState in FindObjectsOfType<ObjectState>(true))
		{
			objectState.SetState();
		}
	}
}
