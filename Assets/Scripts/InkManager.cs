using Ink.Runtime;
using UnityEngine;
using TMPro;
using System.Collections;

public class InkManager : MonoBehaviour
{

	[SerializeField] private TextMeshPro textField = null;
	[SerializeField] private GameObject textBubble = null;
	[SerializeField] private GameSettings Settings = null;
	private Story story = null;
	private bool isStoryActive = false;
	private Coroutine type = null;

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

		string sentence = story.Continue();
		if (type != null) StopCoroutine(type);
		type = StartCoroutine(TypeSentence(sentence));
	}
	private IEnumerator TypeSentence(string sentence)
	{
		textField.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			int timer = 0;
			while (timer <= Settings.TextSpeed)
			{
				timer++;
				yield return null;
			}
			textField.text += letter;
			yield return null;
		}
	}
}