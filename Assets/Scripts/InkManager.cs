using Ink.Runtime;
using UnityEngine;
using TMPro;

public class InkManager : MonoBehaviour
{

	[SerializeField] private TextMeshPro textField = null;
	[SerializeField] private GameObject textBubble = null;
	private Story story = null;
	private bool isStoryActive = false;

	private void StartStory(TextAsset JsonAsset)
	{
		if (isStoryActive) return;

		story = new Story(JsonAsset.text);
		isStoryActive = true;
		textBubble.SetActive(true);
	}

	public void OnClick()
	{
		if (isStoryActive) DisplayNextLine();
	}
	public void OnSubmit()
	{
		if (isStoryActive) DisplayNextLine();
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