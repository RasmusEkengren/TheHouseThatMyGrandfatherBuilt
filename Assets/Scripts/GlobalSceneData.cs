using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSceneData : MonoBehaviour
{
    [Serializable]
    public class DataContainer
    {
        public string name = null;
        public GeorgeState.State georgeState = GeorgeState.State.Porch;
        public GameObject obj;
    }

    public static Vector3 lastLeahPosition;
    public static Quaternion lastLeahRotation;
    public SceneType sceneType;
    public List<DataContainer> containerList = new List<DataContainer>();
    public List<int> testlist = new List<int>();

    public static bool mg_porchFixed;
    public static bool mg_windowsFixed;

    private void Start()
    {

    }
}
