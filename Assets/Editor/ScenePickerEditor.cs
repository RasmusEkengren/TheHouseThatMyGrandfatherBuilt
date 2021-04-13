using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Scene.SceneChanger))]
public class ScenePickerEditor : Editor
{
    SceneAsset scene = null;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
   
        // Save the script that this editor is on
        Scene.SceneChanger sceneChanger = (Scene.SceneChanger)target;

        EditorGUI.BeginChangeCheck();
        // When input has been changed, update the string in SceneChanger with the scene name
        scene = sceneChanger.nextScene;
        sceneChanger.sceneString = sceneChanger.nextScene.name;
        if (sceneChanger.nextScene == null)
        {
            sceneChanger.sceneString = null;
        }
        if (EditorGUI.EndChangeCheck())
        {
            Debug.Log(sceneChanger.gameObject.name + ": sceneString has been set to: " + sceneChanger.nextScene.name, sceneChanger.gameObject);
        }
    }
}
