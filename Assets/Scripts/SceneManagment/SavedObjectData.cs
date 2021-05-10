using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Events;
using FMODUnity;


public class SavedObjectData : GameEventListener
{
    public struct SavedDataContainer
    {
        public enum SceneType {Leah, George}
        public SceneType _sceneType;
        public GameObject _object;
        public bool activeOnSceneStart;

    }

    public SceneType.Scene sceneType;
}

