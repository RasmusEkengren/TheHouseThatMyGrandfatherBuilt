using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawingSound : MonoBehaviour
{
	public void PlaySawingSound()
	{
		GetComponentInParent<Sawing>().PlaySawCutSound();
	}
	public void PlayPlankFallSound()
	{
		GetComponentInParent<Sawing>().PlayPlankFallSound();
	}
}
