using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSceneData : MonoBehaviour
{
    [SerializeField]
    public class DataContainer
    {
        public string name;
        public GameObject obj;
    }

    public SceneType sceneType;

    public List<DataContainer> containerList = new List<DataContainer>();
}
