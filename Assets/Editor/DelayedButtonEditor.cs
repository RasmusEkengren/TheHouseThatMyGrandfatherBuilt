using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(DelayedButtonEvents))]
public class DelayedButtonEditor : Editor
{
    //private Button buttonComponent = null;
    //private Button.ButtonClickedEvent events = null;
    //public override void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();
    //    DelayedButtonEvents buttonEvents = (DelayedButtonEvents)target;

    //    GUILayout.Space(20f);

    //    if (GUILayout.Button("Get Events from Button"))
    //    {
    //        buttonComponent = buttonEvents.GetComponent<Button>();
    //        events = buttonComponent.onClick;
    //        buttonEvents.OnClick = events;
    //        events = null;
    //    }
    //}
}
