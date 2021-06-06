using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTimed : MonoBehaviour
{
	[SerializeField] private GameObject gameObjectToShow;
	[SerializeField] private float timeToShow = 10;
	private float t = 0;

	void Start()
	{
		t = 0;
	}
	void Update()
	{
		if (t < timeToShow)
		{
			gameObjectToShow.SetActive(true);
			t += Time.deltaTime;
		}
		else
		{
			gameObjectToShow.SetActive(false);
		}
	}
}
