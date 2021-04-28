using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionNumber : MonoBehaviour
{
	[SerializeField] private GameSettings settings;
	[SerializeField] private TextMeshProUGUI text;
	void Start()
	{
		text.text = "Version: " + settings.Version;
	}
}
