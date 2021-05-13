using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerFootstepSound : MonoBehaviour
{
	[SerializeField] [FMODUnity.EventRef] private string footstepSound = null;
	[SerializeField] [Range(0, 2)] private int soundParameter = 0;
	private EventInstance footstepInstance;
	[HideInInspector] public GameObject currentCollision = null;

	private void OnEnable()
	{
		footstepInstance = RuntimeManager.CreateInstance(footstepSound);
		footstepInstance.setParameterByName("Surface", soundParameter);
	}

	private void OnDisable()
	{
		footstepInstance.release();
	}

	public void PlayFootstep()
	{
		UpdateCollision();
		footstepInstance.setParameterByName("Surface", soundParameter);
		footstepInstance.start();
	}

	private void UpdateCollision()
	{
		if (currentCollision.tag == "Grass")
		{
			//Debug.Log("Player collided with: Grass");
			soundParameter = 0;
		}
		if (currentCollision.tag == "Mud")
		{
			//Debug.Log("Player collided with: Mud/Path");
			soundParameter = 1;
		}
		if (currentCollision.tag == "Wood")
		{
			//Debug.Log("Player collided with: Wood");
			soundParameter = 2;
		}
	}
}
