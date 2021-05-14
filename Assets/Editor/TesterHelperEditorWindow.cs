using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;


// Video I got help from https://www.youtube.com/watch?v=c_3DXBrH-Is

public class TesterHelperEditorWindow : ExtendedEditorWindow
{
    private PlayerMovement player = null;

    public bool speedToggle = false;
    private float speedrunSpeed = 20f;
    private float speedrunAutoSpeed = 14f;

    private float playerOriginalSpeed = 3f;
    private float playerOriginalAutoSpeed = 3f;

    private SerializedProperty serializedGeorge = null;

    public static void Open(TesterHelperObject testerObject)
    {
        TesterHelperEditorWindow window = GetWindow<TesterHelperEditorWindow>("Tester Helper Window :D");
        window.serializedObject = new SerializedObject(testerObject);
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnExitPlaymode;
        minSize = new Vector2(100f, 100f);
    }

    private void OnDestroy()
    {
        // Return player to normal speed
    }

    private void OnExitPlaymode(PlayModeStateChange stateChange)
    {
        player.ChangeSpeed(playerOriginalSpeed, playerOriginalAutoSpeed);
    }

    private void UpdateStates()
    {
        //GlobalSceneData.leahState = leahState;
        //GlobalSceneData.georgeState = georgeState;
        //GlobalSceneData.porchState = porchState;
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

    [MenuItem("Window/Tester Helper")]
    public static void ShowWindow()
    {
        GetWindow<TesterHelperEditorWindow>("Tester Helper");
        Debug.Log("Hello user! I am your personal Tester Helper, use me to reduce your precious time testing");
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("georgeState");
        DrawProperties(currentProperty, false);

        GUILayout.Space(10f);
        speedToggle = GUILayout.Toggle(speedToggle, "Increase Movement Speed");

        if (EditorApplication.isPlaying)
        {
            ToggleSpeed();
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

    }

    private void ClearInteracts()
    {
        GlobalSceneData.interactedObjectIDs.Clear();
    }
}
