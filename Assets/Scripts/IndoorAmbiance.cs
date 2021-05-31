using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class IndoorAmbiance : MonoBehaviour
{
	[SerializeField] private bool isIndoors = true;
	void Start()
	{
		FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Location", isIndoors ? 1 : 0);
	}
}
