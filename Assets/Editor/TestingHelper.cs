using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TestingHelper : EditorWindow
{
    
    private Vector2 MaxSize;


    private void OnEnable()
    {

    }

    private void OnDestroy()
    {

    }

    [MenuItem("Window/Tester Helper")]
    public static void ShowWindow()
    {
        GetWindow<TestingHelper>("Tester Helper");
        Debug.Log("Hello user! I am your personal Tester Helper, use me to reduce your precious time testing");
    }

    private void OnGUI()
    {
        if (EditorApplication.isPlaying)
        {
            GUILayout.Space(10f);
            if (GUILayout.Button("Speedrun mode"))
            {
                Debug.Log("Speedrun mode engaged! (Character speeds increased)");
                PlayerMovement player = FindObjectOfType<PlayerMovement>();
            }
            GUILayout.Space(10f);
            if (GUILayout.Button("Load Leah Scene"))
            {
                SceneManager.LoadScene("Leah");
                Debug.Log("Hoping over to Leah's scene!");
            }
            GUILayout.Space(10f);
            if (GUILayout.Button("Load George Scene"))
            {
                SceneManager.LoadScene("George");
                Debug.Log("Hoping over to George's scene!");
            }
            GUILayout.Space(10f);

            if (GUILayout.Button("Clear Interacts"))
            {
                ClearInteracts();
            }
        }
    }

    private void ClearInteracts()
    {
        GlobalSceneData.interactedObjectIDs.Clear();
    }
}
