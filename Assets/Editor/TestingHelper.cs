using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class TestingHelper : EditorWindow
{
    private Vector2 MaxSize;
    private PlayerMovement player = null;

    private bool speedToggle = false;
    private float speedrunSpeed = 20f;
    private float speedrunAutoSpeed = 14f;

    private float playerOriginalSpeed = 3f;
    private float playerOriginalAutoSpeed = 3f;

    [SerializeField] public GlobalSceneData.LeahState leahState;
    public GlobalSceneData.GeorgeState georgeState;
    public GlobalSceneData.PorchState porchState;

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnExitPlaymode;
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
        GlobalSceneData.leahState = leahState;
        GlobalSceneData.georgeState = georgeState;
        GlobalSceneData.porchState = porchState;
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
        GetWindow<TestingHelper>("Tester Helper");
        Debug.Log("Hello user! I am your personal Tester Helper, use me to reduce your precious time testing");
    }

    private void OnGUI()
    {
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
