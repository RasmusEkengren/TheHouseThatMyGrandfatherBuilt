using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObjects/Tester Helper Object", fileName = "TesterHelperObject")]
public class TesterHelperObject : ScriptableObject
{
	public GlobalSceneData.LeahState leahState = GlobalSceneData.LeahState.Entering;
	public GlobalSceneData.GeorgeState georgeState = GlobalSceneData.GeorgeState.Porch;
	public GlobalSceneData.PorchFixingState porchFixingState = GlobalSceneData.PorchFixingState.Broken;
	public GlobalSceneData.PorchStyle porchStyle = GlobalSceneData.PorchStyle.None;
	public GlobalSceneData.WindowsFixingState windowsFixingState = GlobalSceneData.WindowsFixingState.Broken;
	public GlobalSceneData.WindowsStyle windowsStyle = GlobalSceneData.WindowsStyle.None;
	public GlobalSceneData.RailingFixingState railingFixingState = GlobalSceneData.RailingFixingState.Broken;
	public GlobalSceneData.RailingStyle railingStyle = GlobalSceneData.RailingStyle.None;

	public void GetStates()
	{
		leahState = GlobalSceneData.leahState;
		georgeState = GlobalSceneData.georgeState;
		porchFixingState = GlobalSceneData.porchFixingState;
		porchStyle = GlobalSceneData.porchStyle;
		windowsFixingState = GlobalSceneData.windowsFixingState;
		windowsStyle = GlobalSceneData.windowsStyle;
		railingFixingState = GlobalSceneData.railingFixingState;
		railingStyle = GlobalSceneData.railingStyle;
	}
}
