using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// This is a super bare bones example of how to play and display a ink story in Unity.
[RequireComponent(typeof(AudioSource))]
public class BasicInkExample : MonoBehaviour {
    public static event Action<Story> OnCreateStory;
	AudioSource m_MyAudioSource;
	public AudioClip[] voicelines;
	Dictionary<string, AudioClip> voiceClipDict = new Dictionary<string, AudioClip>();

	void Awake () {
		// Remove the default message
		RemoveChildren();
		StartStory();
	}

	void Start()
    {
		m_MyAudioSource = GetComponent<AudioSource>();
		for (int i = 0; i < voicelines.Length; i++)
        {
			AudioClip clip = voicelines[i];
			voiceClipDict.Add(clip.name, clip);
        }
	}

	void Update()
    {
		if (story.canContinue && story.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.Space))
        {
			RefreshView();
		}
    }

	// Creates a new Story object with the compiled story which we can then play!
	void StartStory () {
		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}
	
	string storySubTag(string subtag)
    {
		List<string> tags = story.currentTags;
		string value = tags.Where(t => t.StartsWith(subtag + ":")).FirstOrDefault();
		if(value != null)
        {
			return value.Substring(subtag.Length + 1);
        }
		return null;
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		if (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue ();
			string voiceLine = storySubTag("vl");
			if (voiceLine != null)
            {
				try
				{
					AudioClip clip = voiceClipDict[voiceLine];
					m_MyAudioSource.clip = clip;
					m_MyAudioSource.Play(0);
				}
				catch (KeyNotFoundException) 
				{
					Debug.Log("Couldn't find audioClip " + voiceLine);
				}
			}
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if (story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
		else if (story.canContinue)
        {
			Text continueText = Instantiate(continueTextPrefab) as Text;
			continueText.text = "Press Space to continue...";
			continueText.transform.SetParent(canvas.transform, false);
        }
		// If we've read all the content and there's no choices, the story is finished!
		else {
			Button choice = CreateChoiceView("End of story.\nRestart?");
			choice.onClick.AddListener(delegate{
				StartStory();
			});
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView (string text) {
		Text storyText = Instantiate (textPrefab) as Text;
		storyText.text = text;
		storyText.transform.SetParent (canvas.transform, false);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}

	[SerializeField]
	private TextAsset inkJSONAsset = null;
	public Story story;

	[SerializeField]
	private Canvas canvas = null;

	// UI Prefabs
	[SerializeField]
	private Text textPrefab = null;
	[SerializeField]
	private Text continueTextPrefab = null;
	[SerializeField]
	private Button buttonPrefab = null;
}
