using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneLevelEditor : EditorWindow
{
    List<GameObject> list = new List<GameObject>();
    List<GameObject> objectsUsingLevels = new List<GameObject>();
    // bool addOnce = false;
    Scene scene;

    private void OnEnable()
    {
        list.Capacity = scene.rootCount +1;
        SceneManager.GetActiveScene();
        scene.GetRootGameObjects(list);
    }

    private void OnDestroy()
    {
        list.Clear();
    }

    [MenuItem("Window/Scene Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<SceneLevelEditor>("Scene Level Editor");
        if (!SceneController.instance)
        {
            Debug.LogWarning("No SceneController in this scene");
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(10f);

        if (GUILayout.Button("Refresh object list"))
        {
            Refresh();
        }

        GUILayout.Space(10f);


        GUILayout.Space(15f);

        if (GUILayout.Button("Increase Level"))
        {
            //if (SceneController.currentSceneLevel <= 9)
            //{
            //    Debug.Log("Increased scene level: " + SceneController.currentSceneLevel);
            //    SceneController.currentSceneLevel += 1;
            //    UpdateAllObjects();
            //}
        }

        GUILayout.Space(10f);

        if (GUILayout.Button("Decrease Level"))
        {
            //if (SceneController.currentSceneLevel >= 1)
            //{
            //    Debug.Log("Decreased scene level: " + SceneController.currentSceneLevel);
            //    SceneController.currentSceneLevel -= 1;
            //    UpdateAllObjects();
            //}
        }
    }

    private void Refresh()
    {
        scene.GetRootGameObjects(list);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].gameObject.GetComponent<ObjectSceneLevel>())
            {
                objectsUsingLevels.Add(list[i]);
            }
        }
    }

    private void UpdateAllObjects()
    {
        foreach(GameObject obj in objectsUsingLevels)
        {
            obj.GetComponent<ObjectSceneLevel>().UpdateState();
        }
    }

    /// What I want:
    /// Buttons to increase, decrease scene levels
    /// When changing level: Update all objects that use it
    /// 
}
