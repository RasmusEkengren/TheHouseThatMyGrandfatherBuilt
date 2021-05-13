using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Tester Helper Object", fileName = "TesterHelperObject")]
public class TesterHelperObject : ScriptableObject
{
    [SerializeField]
    public class testContainer
    {
        public int number = 1;
        public string name = "Test";
    }

    public List<testContainer> testList = new List<testContainer>();
    public List<int> testList2 = new List<int>();
    public int testInt = 42;
    public GlobalSceneData.GeorgeState georgeState;
    public GlobalSceneData.LeahState leahState;
    public GlobalSceneData.PorchState porchState;
}
