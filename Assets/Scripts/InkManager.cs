using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;
using Ink.Runtime;
using System.Collections;

public class InkManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textField = null;
	[SerializeField] private GameObject textBubble = null;
	[SerializeField] private GameSettings Settings = null;
	private Story story = null;
	private bool isStoryActive = false;
	private Coroutine type = null;
	private UnityEvent endEvent;
	public void StartStory(TextAsset JsonAsset)
	{
		if (isStoryActive) return;

		story = new Story(JsonAsset.text);
		isStoryActive = true;
		textBubble.SetActive(true);
	}

	public void OnInput(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed && isStoryActive) DisplayNextLine();
	}
	public void SetEndEvent(UnityEvent end)
	{
		endEvent = end;
	}
	public void DisplayNextLine()
	{
		if (!story.canContinue)
		{
			isStoryActive = false;
			textBubble.SetActive(false);
			if (endEvent != null)
			{
				endEvent.Invoke();
				endEvent = null;
			}
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