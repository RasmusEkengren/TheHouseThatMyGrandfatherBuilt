using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class TesterHelperEditorWindow : EditorWindow
{
    private PlayerMovement player = null;

    public bool speedToggle = false;
    private float speedrunSpeed = 20f;
    private float speedrunAutoSpeed = 14f;

    private float playerOriginalSpeed = 3f;
    private float playerOriginalAutoSpeed = 3f;

    TesterHelperObject THObject = null;

    public SerializedObject so;
    public SerializedProperty propGeorge = null;
    public SerializedProperty propLeah = null;
    public SerializedProperty propPorch = null;

    [MenuItem("Tools/Tester Helper")]
    public static void OpenWindow()
    {
        GetWindow<TesterHelperEditorWindow>("Tester Helper");
        Debug.Log("Hello user! I am your personal Tester Helper, use me to reduce your precious time testing");
    }

    //public static void Open(TesterHelperObject testerObject)
    //{
    //    TesterHelperEditorWindow window = GetWindow<TesterHelperEditorWindow>("Tester Helper Window :D");
    //    // window.serializedObject = new SerializedObject(testerObject);
    //}

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnExitPlaymode;
        SceneManager.sceneLoaded += OnSceneLoaded;

        minSize = new Vector2(100f, 100f);

        THObject = ScriptableObject.CreateInstance<TesterHelperObject>();
        so = new SerializedObject(THObject);

        propLeah = so.FindProperty("leahState");
        propGeorge = so.FindProperty("georgeState");
        propPorch = so.FindProperty("porchState");
    }

    private void OnDestroy()
    {
        // Return player to normal speed
    }

    private void OnExitPlaymode(PlayModeStateChange stateChange)
    {
        player.ChangeSpeed(playerOriginalSpeed, playerOriginalAutoSpeed);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        THObject.GetStates();
    }

    private void UpdateStates()
    {
        GlobalSceneData.georgeState = THObject.georgeState;
        GlobalSceneData.leahState = THObject.leahState;
        GlobalSceneData.porchState = THObject.porchState;
    }

    private void ToggleSpeed()
    {
        if (speedToggle)
        {
            player = FindObjectOfType<PlayerMovement>();
            player.ChangeSpeed(speedrunSpeed, speedrunAutoSpeed);
        }
        else
        {
            speedToggle = false;
            player = FindObjectOfType<PlayerMovement>();
            player.ChangeSpeed(playerOriginalSpeed, playerOriginalAutoSpeed);
        }
    }

    private void OnGUI()
    {
        so.Update();

        GUILayout.Space(10f);
        speedToggle = GUILayout.Toggle(speedToggle, "Increase Movement Speed");
        GUILayout.Space(10f);

     
        EditorGUILayout.PropertyField(propLeah);
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(propGeorge);
        GUILayout.Space(5f);
        EditorGUILayout.PropertyField(propPorch);

        if (EditorApplication.isPlaying)
        {
            ToggleSpeed();
            UpdateStates();
        }

        GUILayout.Space(10f);
        if (GUILayout.Button("Load Leah Scene"))
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

        GUILayout.Space(10f);
        if (GUILayout.Button("Load George Scene"))
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
        GUILayout.Space(10f);

        if (GUILayout.Button("Clear Interacts"))
        {
            ClearInteracts();
        }

        so.ApplyModifiedProperties();
    }

    private void ClearInteracts()
    {
        GlobalSceneData.interactedObjectIDs.Clear();
    }
}
