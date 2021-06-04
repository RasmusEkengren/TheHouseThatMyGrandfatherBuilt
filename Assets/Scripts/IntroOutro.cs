using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;
using Ink.Runtime;
using System.Collections;

public class IntroOutro : MonoBehaviour
{
	[SerializeField] private Image[] pictures;
	[SerializeField] private TextAsset[] stories;
	[SerializeField] private TextMeshProUGUI textField;
	[SerializeField] private SceneChanger sceneChanger;
	[SerializeField] private GameObject quitText;
	[SerializeField] private float fadeDuration = 1f;
	[SerializeField] private GameSettings Settings = null;
	[SerializeField] private UnityEvent finalTextEvent;
	[SerializeField] private UnityEvent creditsEvent;
	private Story story = null;
	private int currentStory = 0;
	private Coroutine type = null;
	private bool isTyping = false;
	private string sentence = "";
	private bool canQuit = false;
	private void StartStory(TextAsset JsonAsset)
	{
		story = new Story(JsonAsset.text);
		DisplayNextLine();
		StartCoroutine(Fade());
	}
	void Start()
	{
		StartStory(stories[0]);
	}
	public void OnInput(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed) DisplayNextLine();
	}
	public void Quit(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed && canQuit) sceneChanger.ChangeScene();
	}
	public void DisplayNextLine()
	{
		if (isTyping)
		{
			textField.text = sentence;
			if (type != null) StopCoroutine(type);
			isTyping = false;
			return;
		}
		if (!story.canContinue)
		{
			currentStory++;
			if (currentStory >= stories.Length)
			{
				finalTextEvent.Invoke();
				StartCoroutine(QuitTimer());
			}
			if (currentStory < stories.Length) StartStory(stories[currentStory]);
			return;
		}
		sentence = story.Continue();
		if (type != null) StopCoroutine(type);
		type = StartCoroutine(TypeSentence(sentence));
	}
	public void StartTimedFade(float f)
	{
		StartCoroutine(TimedFade(f));
	}
	private IEnumerator TypeSentence(string sentence)
	{
		textField.text = "";
		isTyping = true;
		float t = 0;
		int currentLetter = 0;
		float sentenceTime = sentence.Length * Settings.TextPrintLength;
		while (t <= sentenceTime)
		{
			if (t > currentLetter * Settings.TextPrintLength)
			{
				textField.text += sentence[currentLetter];
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
	private IEnumerator Fade()
	{
		int curStory = currentStory;
		if (curStory == 0)
		{
			Color startCol = pictures[0].color;
			Color targetCol = new Color(startCol.r, startCol.g, startCol.b, 1f);
			float timer = 0f;
			while (timer < fadeDuration)
			{
				timer += Time.deltaTime;
				Color currentCol = Color.Lerp(startCol, targetCol, timer / fadeDuration);
				pictures[0].color = currentCol;
				yield return null;
			}
		}
		else
		{
			Color startCol1 = pictures[curStory - 1].color;
			Color targetCol1 = new Color(startCol1.r, startCol1.g, startCol1.b, 0f);
			Color startCol2 = pictures[curStory].color;
			Color targetCol2 = new Color(startCol2.r, startCol2.g, startCol2.b, 1f);
			float timer = 0f;
			while (timer < fadeDuration)
			{
				timer += Time.deltaTime;
				Color currentCol1 = Color.Lerp(startCol1, targetCol1, timer / fadeDuration);
				Color currentCol2 = Color.Lerp(startCol2, targetCol2, timer / fadeDuration);
				pictures[curStory - 1].color = currentCol1;
				pictures[curStory].color = currentCol2;
				yield return null;
			}
		}
	}
	private IEnumerator TimedFade(float t)
	{
		Color startCol1 = pictures[1].color;
		Color targetCol1 = new Color(startCol1.r, startCol1.g, startCol1.b, 0f);
		float timer = 0f;
		while (timer < t)
		{
			timer += Time.deltaTime;
			Color currentCol1 = Color.Lerp(startCol1, targetCol1, timer / t);
			pictures[1].color = currentCol1;
			yield return null;
		}
		creditsEvent.Invoke();
	}
	private IEnumerator QuitTimer()
	{
		float t = 0;
		while (t <= 8)
		{
			t += Time.deltaTime;
			yield return null;
		}
		canQuit = true;
		quitText.SetActive(true);
	}
}
