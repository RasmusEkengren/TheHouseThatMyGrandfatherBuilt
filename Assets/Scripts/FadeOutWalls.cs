using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FadeOutWalls : MonoBehaviour
{
	[Serializable]
	private class FadeOutObject
	{
		public Material normalMaterial;
		public Material fadeOutMaterial;
		public MeshRenderer[] objects;
	}
	[SerializeField] private FadeOutObject[] fadeOutObjects;
	[SerializeField] private float alpha = 0.1f;
	private string playerTag = "Player";

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == playerTag)
		{
			Fade(true);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == playerTag)
		{
			Fade(false);
		}
	}
	private void Fade(bool isFading)
	{
		foreach (FadeOutObject fadeObject in fadeOutObjects)
		{
			foreach (MeshRenderer renderer in fadeObject.objects)
			{
				if (isFading)
				{
					renderer.material = fadeObject.fadeOutMaterial;
					fadeObject.fadeOutMaterial.color = new Color(fadeObject.fadeOutMaterial.color.r, fadeObject.fadeOutMaterial.color.g, fadeObject.fadeOutMaterial.color.b, alpha);
				}
				else
				{
					renderer.material = fadeObject.normalMaterial;
				}
			}
		}
	}
}
