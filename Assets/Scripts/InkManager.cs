using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InkManager : MonoBehaviour
{

	[SerializeField] private TextMeshPro textField;
	[SerializeField] private GameObject textBubble;
	private Story story;
	private bool isStoryActive;

	private void StartStory(TextAsset JsonAsset)
	{
		if (isStoryActive) return;

		story = new Story(JsonAsset.text);
		isStoryActive = true;
		textBubble.SetActive(true);
		DisplayNextLine();
	}

	public void OnClick()
	{
		DisplayNextLine();
	}
	public void OnSubmit()
	{
		DisplayNextLine();
	}

	public void DisplayNextLine()
	{
		if (!story.canContinue)
		{
			isStoryActive = false;
			textBubble.SetActive(false);
			return;
		}

		string text = story.Continue(); // gets next line
		text = text?.Trim(); // removes white space from text
		textField.text = text; // displays new text
	}
}