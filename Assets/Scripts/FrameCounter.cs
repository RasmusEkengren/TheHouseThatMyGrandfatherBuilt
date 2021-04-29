using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameCounter : MonoBehaviour
{
	public TextMeshProUGUI display_Text;
	float count;
	IEnumerator Start()
	{
		GUI.depth = 2;
		while (true)
		{
			if (Time.timeScale == 1)
			{
				yield return new WaitForSeconds(0.1f);
				count = (1 / Time.deltaTime);
				display_Text.text = "FPS :" + (Mathf.Round(count));
			}
			else
			{
				display_Text.text = "Pause";
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
}
