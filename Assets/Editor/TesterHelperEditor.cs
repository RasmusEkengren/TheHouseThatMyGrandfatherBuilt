using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TesterHelperObject))]
public class TesterHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Tester Helper!"))
        {
            TesterHelperEditorWindow.Open((TesterHelperObject)target);
        }
    }
}
