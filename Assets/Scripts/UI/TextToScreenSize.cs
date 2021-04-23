using UnityEngine;
using TMPro;

public class TextToScreenSize : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textField;
	void Update()
	{
		if (Camera.main.aspect < 1.7) textField.fontSize = Screen.height / 20.3f;
		else if (Camera.main.aspect < 2.0) textField.fontSize = Screen.height / 21.75f;
		else if (Camera.main.aspect >= 2.0) textField.fontSize = Screen.height / 20.3f;
	}
}
