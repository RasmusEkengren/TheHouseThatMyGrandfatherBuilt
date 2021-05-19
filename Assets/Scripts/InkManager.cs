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
	[System.NonSerialized] public bool isTimedStoryActive = false;
	[SerializeField] private TextMeshProUGUI textBubbleTextField = null;
	[SerializeField] private TextMeshProUGUI cutsceneTextField = null;
	[SerializeField] private GameObject textBubble = null;
	[SerializeField] private GameObject cutscenePanel = null;
	[SerializeField] private GameSettings Settings = null;
	private Story story = null;
	private Coroutine type = null;
	private UnityEvent endEvent;
	private bool isTyping = false;
	private bool isSkippingResetEvent = false;
	private string sentence = "";
	private string functionToCall = null;
	private float timedStoryLength = 10;
	private float timer = 0;
	public void StartStory(TextAsset JsonAsset)
	{
		if (isStoryActive || isCutsceneActive || isTimedStoryActive) return;
		story = new Story(JsonAsset.text);
		isStoryActive = true;
		textBubble.SetActive(true);
	}
	public void StartTimedStory(TextAsset JsonAsset)
	{
		if (isTimedStoryActive) return;
		story = new Story(JsonAsset.text);
		isTimedStoryActive = true;
		textBubble.SetActive(true);
		sentence = story.Continue();
		if (type != null) StopCoroutine(type);
		type = StartCoroutine(TypeSentence(sentence));
	}
	public void StartCutscene(TextAsset JsonAsset)
	{
		if (isCutsceneActive) return;

		story = new Story(JsonAsset.text);
		if (functionToCall != null) story.EvaluateFunction(functionToCall);
		isCutsceneActive = true;
		cutscenePanel.SetActive(true);
		DisplayNextLine();
	}
	public void SetStoryFunction(string functionName)
	{
		functionToCall = functionName;
	}
	public void SetTimedStoryLength(float t)
	{
		timedStoryLength = t;
	}
	public void OnInput(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed && (isStoryActive || isCutsceneActive || isTimedStoryActive)) DisplayNextLine();
	}
	public void SetEndEvent(UnityEvent end)
	{
		endEvent = end;
	}
	public void SkipResetEvent()
	{
		isSkippingResetEvent = true;
	}
	public void DisplayNextLine()
	{
		if (isTyping)
		{
			if (isCutsceneActive) cutsceneTextField.text = sentence;
			else textBubbleTextField.text = sentence;
			if (type != null) StopCoroutine(type);
			isTyping = false;
			return;
		}
		if (isTimedStoryActive) return;
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
				if (isSkippingResetEvent)
				{
					isSkippingResetEvent = false;
				}
				else
				{
					endEvent = null;
				}
			}
			return;
		}
		sentence = story.Continue();
		if (type != null) StopCoroutine(type);
		type = StartCoroutine(TypeSentence(sentence));
	}
	void Update()
	{
		if (isTimedStoryActive)
		{
			timer += Time.deltaTime;
			if (timer >= timedStoryLength)
			{
				if (!story.canContinue)
				{
					isTimedStoryActive = false;
					textBubble.SetActive(false);
					if (endEvent != null)
					{
						endEvent.Invoke();
						if (isSkippingResetEvent)
						{
							isSkippingResetEvent = false;
						}
						else
						{
							endEvent = null;
						}
					}
					return;
				}
				sentence = story.Continue();
				if (type != null) StopCoroutine(type);
				type = StartCoroutine(TypeSentence(sentence));
				timer = 0;
			}
		}
	}
	private IEnumerator TypeSentence(string sentence)
	{
		if (isCutsceneActive) cutsceneTextField.text = "";
		else textBubbleTextField.text = "";
		isTyping = true;
		float t = 0;
		int currentLetter = 0;
		float sentenceTime = sentence.Length * Settings.TextPrintLength;
		while (t <= sentenceTime)
		{
			if (t > currentLetter * Settings.TextPrintLength)
			{
				if (isCutsceneActive) cutsceneTextField.text += sentence[currentLetter];
				else textBubbleTextField.text += sentence[currentLetter];
				currentLetter++;
			}
			else
			{
				t += Time.deltaTime;
				yield return null;
			}
		}
		isTyping = false;
	}
}