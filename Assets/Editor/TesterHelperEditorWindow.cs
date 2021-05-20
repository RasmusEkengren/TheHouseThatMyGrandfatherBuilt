using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.AssetImporters;

public class TesterHelperEditorWindow : EditorWindow
{
	private PlayerMovement player = null;
	private Vector2 scrollPos;

	private bool speedToggle = false;
	private float speedrunSpeed = 20f;
	private float speedrunAutoSpeed = 14f;

	private float playerOriginalSpeed = 3f;
	private float playerOriginalAutoSpeed = 3f;

	TesterHelperObject THObject = null;

	public SerializedObject so = null;
	public SerializedProperty propGeorge = null;
	public SerializedProperty propLeah = null;
	public SerializedProperty propPorch = null;
	public SerializedProperty propWindow = null;
	public SerializedProperty propRailing = null;
	private bool _PorchFixed = false;
	private bool _PorchFixing = false;
	private bool _WindowsFixed = false;
	private bool _WindowsFixing = false;
	private bool _RailingFixed = false;
	private bool _RailingFixing = false;

	[MenuItem("Tools/Tester Helper�")]
	public static void OpenWindow()
	{
		GetWindow<TesterHelperEditorWindow>("Tester Helper�");
		Debug.Log("Hello user! I am your personal Tester Helper, use me to reduce your precious time testing");
	}

	private void OnEnable()
	{
		EditorApplication.playModeStateChanged += OnExitPlaymode;
		SceneManager.sceneLoaded += OnSceneLoaded;

		minSize = new Vector2(248f, 230f);
		maxSize = new Vector2(550f, 500f);

		THObject = ScriptableObject.CreateInstance<TesterHelperObject>();
		if (so == null) { so = new SerializedObject(THObject); }

		propLeah = so.FindProperty("leahState");
		propGeorge = so.FindProperty("georgeState");
		propPorch = so.FindProperty("porchState");
		propWindow = so.FindProperty("windowState");
		propRailing = so.FindProperty("railingState");
	}

	private void OnExitPlaymode(PlayModeStateChange stateChange)
	{
		if (player)
		{
			player.ChangeSpeed(playerOriginalSpeed, playerOriginalAutoSpeed);
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		THObject.GetStates();
		_PorchFixed = GlobalSceneData.mg_porchFixed;
		_PorchFixing = GlobalSceneData.mg_porchFixing;
		_WindowsFixed = GlobalSceneData.mg_windowsFixed;
		_WindowsFixing = GlobalSceneData.mg_windowsFixing;
		_RailingFixed = GlobalSceneData.mg_railingFixed;
		_RailingFixing = GlobalSceneData.mg_railingFixing;
	}

	private void UpdateStates()
	{
		GlobalSceneData.georgeState = THObject.georgeState;
		GlobalSceneData.leahState = THObject.leahState;
		GlobalSceneData.porchState = THObject.porchState;
		GlobalSceneData.windowsState = THObject.windowState;
		GlobalSceneData.railingState = THObject.railingState;
		GlobalSceneData.mg_porchFixed = _PorchFixed;
		GlobalSceneData.mg_porchFixing = _PorchFixing;
		GlobalSceneData.mg_windowsFixed = _WindowsFixed;
		GlobalSceneData.mg_windowsFixing = _WindowsFixing;
		GlobalSceneData.mg_railingFixed = _RailingFixed;
		GlobalSceneData.mg_railingFixing = _RailingFixing;
	}

	private void ToggleSpeed()
	{
		player = FindObjectOfType<PlayerMovement>();
		if (player)
		{
			if (speedToggle)
			{

				player.ChangeSpeed(speedrunSpeed, speedrunAutoSpeed);
			}
			else
			{
				speedToggle = false;
				player = FindObjectOfType<PlayerMovement>();
				player.ChangeSpeed(playerOriginalSpeed, playerOriginalAutoSpeed);
			}
		}
	}

	private void OnGUI()
	{
		if (so != null && EditorApplication.isPlaying)
		{
			so.Update();
		}
		GUILayout.Label("In-game Modifiers");
		GUILayout.Space(10f);
		speedToggle = GUILayout.Toggle(speedToggle, "Increase Movement Speed");
		GUILayout.Space(10f);

		GUILayout.Label("Current game progress (change in-game)");
		scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
		EditorGUILayout.PropertyField(propLeah);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propGeorge);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propPorch);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propWindow);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propRailing);
		GUILayout.Space(5f);
		_PorchFixed = GUILayout.Toggle(_PorchFixed, "Porch has been Fixed");
		GUILayout.Space(5f);
		_PorchFixing = GUILayout.Toggle(_PorchFixing, "Leah is currently fixing the Porch");
		GUILayout.Space(5f);
		_WindowsFixed = GUILayout.Toggle(_WindowsFixed, "Windows have been Fixed");
		GUILayout.Space(5f);
		_WindowsFixing = GUILayout.Toggle(_WindowsFixing, "Leah is currently fixing the Windows");
		GUILayout.Space(5f);
		_RailingFixed = GUILayout.Toggle(_RailingFixed, "Railing has been Fixed");
		GUILayout.Space(5f);
		_RailingFixing = GUILayout.Toggle(_RailingFixing, "Leah is currently fixing the Railing");

		GUILayout.EndScrollView();
		if (EditorApplication.isPlaying)
		{
			ToggleSpeed();
			UpdateStates();
		}

		GUILayout.Space(10f);
		GUILayout.Label("Scene Changing");
		GUILayout.Label("(Load after state changes to apply them)");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Load Leah Scene", GUILayout.Height(40f)))
		{
			if (EditorApplication.isPlaying)
			{
				SceneManager.LoadScene("Leah");
				Debug.Log("Hoping over to Leah's scene!");
			}
			else if (!EditorApplication.isPlaying && !EditorApplication.isCompiling)
			{
				EditorSceneManager.OpenScene("Assets/Scenes/Leah.unity");
			}
		}

		GUILayout.Space(5f);
		if (GUILayout.Button("Load George Scene", GUILayout.Height(40f)))
		{
			if (EditorApplication.isPlaying)
			{
				SceneManager.LoadScene("George");
				Debug.Log("Hoping over to Georges's scene!");
			}
			else if (!EditorApplication.isPlaying && !EditorApplication.isCompiling)
			{
				EditorSceneManager.OpenScene("Assets/Scenes/George.unity");
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);

		//if (GUILayout.Button("Clear Interacts", GUILayout.Height(30f)))
		//{
		//    ClearInteracts();
		//}

		if (so != null && EditorApplication.isPlaying)
		{
			so.ApplyModifiedProperties();
		}
	}

	private void ClearInteracts()
	{
		GlobalSceneData.interactedObjectIDs.Clear();
	}
}
