using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Scene.SceneChanger))]
public class ScenePickerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
   
        // Save the script that this editor is on
        Scene.SceneChanger sceneChanger = (Scene.SceneChanger)target;

        EditorGUI.BeginChangeCheck();
        // When input has been changed, update the string in SceneChanger with the scene name

        if (sceneChanger.nextScene && sceneChanger.nextScene.name.Length > 0)
        {
            sceneChanger.sceneString = sceneChanger.nextScene.name;
        }
        else
        {
            sceneChanger.sceneString = null;
        }
        if (EditorGUI.EndChangeCheck())
        {
            Debug.Log(sceneChanger.gameObject.name + ": sceneString has been set to: " + sceneChanger.nextScene.name, sceneChanger.gameObject);
        }
    }
}
