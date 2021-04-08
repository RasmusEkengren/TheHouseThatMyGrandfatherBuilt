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

        // When input has been changed, update the string in SceneChanger with the scene name
        sceneChanger.sceneString = sceneChanger.nextScene.name;

       // The inspector gets updated about 8+ times on each click. Best not use logs until that is fixed
       // Debug.Log(sceneChanger.gameObject.name + ": SceneChanger - sceneString has been set to:" + sceneChanger.nextScene.name);
    }
}
