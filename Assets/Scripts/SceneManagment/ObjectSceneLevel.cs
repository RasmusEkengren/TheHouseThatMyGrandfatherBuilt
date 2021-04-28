using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Events;
using FMODUnity;


public class ObjectSceneLevel : GameEventListener
{
    public SceneType.Scene sceneType;

    private void Start()
    {
        UpdateState();
    }

    public void UpdateState()
    {
    }
}

