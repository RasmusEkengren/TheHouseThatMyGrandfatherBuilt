using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;
using Ink.Runtime;
using System.Collections;

public class InkManager : MonoBehaviour
{
	[System.NonSerialized] public bool isStoryActive = false;
	[System.NonSerialized] public bool isCutsceneActive = false;
	[SerializeField] private TextMeshProUGUI textBubbleTextField = null;
	[SerializeField] private TextMeshProUGUI cutsceneTextField = null;
	[SerializeField] private GameObject textBubble = null;
	[SerializeField] private GameObject cutscenePanel = null;
	[SerializeField] private GameSettings Settings = null;
	private Story story = null;
	private Coroutine type = null;
	private UnityEvent endEvent;
	public void StartStory(TextAsset JsonAsset)
	{
		if (isStoryActive || isCutsceneActive) return;

		story = new Story(JsonAsset.text);
		isStoryActive = true;
		textBubble.SetActive(true);
	}
	public void StartCutscene(TextAsset JsonAsset)
	{
		if (isCutsceneActive) return;

		story = new Story(JsonAsset.text);
		isCutsceneActive = true;
		cutscenePanel.SetActive(true);
		DisplayNextLine();
	}
	public void OnInput(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed && (isStoryActive || isCutsceneActive)) DisplayNextLine();
	}
	public void SetEndEvent(UnityEvent end)
	{
		endEvent = end;
	}
	public void DisplayNextLine()
	{
		if (!story.canContinue)
		{
			if (isCutsceneActive)
			{
				isCutsceneActive = false;
				cutscenePanel.SetActive(false);
			}
			else
			{
				isStoryActive = false;
				textBubble.SetActive(false);
			}
			if (endEvent != null)
			{
				endEvent.Invoke();
			}
			return;
		}
		string sentence = story.Continue();
		if (type != null) StopCoroutine(type);
		type = StartCoroutine(TypeSentence(sentence));
	}
	private IEnumerator TypeSentence(string sentence)
	{
		if (isCutsceneActive) cutsceneTextField.text = "";
		else textBubbleTextField.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			int timer = 0;
			while (timer <= Settings.TextSpeed)
			{
				timer++;
				yield return null;
			}
			if (isCutsceneActive) cutsceneTextField.text += letter;
			else textBubbleTextField.text += letter;
			yield return null;
		}
	}
}