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
	public SerializedProperty propPorchFixing = null;
	public SerializedProperty propPorchStyle = null;
	public SerializedProperty propWindowsFixing = null;
	public SerializedProperty propWindowsStyle = null;
	public SerializedProperty propRailingsFixing = null;
	public SerializedProperty propRailingStyle = null;

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
		propPorchFixing = so.FindProperty("porchFixingState");
		propPorchStyle = so.FindProperty("porchStyle");
		propWindowsFixing = so.FindProperty("windowsFixingState");
		propWindowsStyle = so.FindProperty("windowsStyle");
		propRailingsFixing = so.FindProperty("railingFixingState");
		propRailingStyle = so.FindProperty("railingStyle");
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
	}

	private void UpdateStates()
	{
		GlobalSceneData.georgeState = THObject.georgeState;
		GlobalSceneData.leahState = THObject.leahState;
		GlobalSceneData.porchFixingState = THObject.porchFixingState;
		GlobalSceneData.porchStyle = THObject.porchStyle;
		GlobalSceneData.windowsFixingState = THObject.windowsFixingState;
		GlobalSceneData.windowsStyle = THObject.windowsStyle;
		GlobalSceneData.railingFixingState = THObject.railingFixingState;
		GlobalSceneData.railingStyle = THObject.railingStyle;
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
		EditorGUILayout.PropertyField(propPorchFixing);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propPorchStyle);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propWindowsFixing);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propWindowsStyle);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propRailingsFixing);
		GUILayout.Space(5f);
		EditorGUILayout.PropertyField(propRailingStyle);
		GUILayout.Space(5f);

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
