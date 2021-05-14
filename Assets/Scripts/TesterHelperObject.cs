using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObjects/Tester Helper Object", fileName = "TesterHelperObject")]
public class TesterHelperObject : ScriptableObject
{
    public GlobalSceneData.LeahState leahState = GlobalSceneData.LeahState.Entering;
    public GlobalSceneData.GeorgeState georgeState = GlobalSceneData.GeorgeState.Porch;
    public GlobalSceneData.PorchState porchState = GlobalSceneData.PorchState.Broken;

    public void GetStates()
    {
        leahState = GlobalSceneData.leahState;
        georgeState = GlobalSceneData.georgeState;
        porchState = GlobalSceneData.porchState;
    }

}
