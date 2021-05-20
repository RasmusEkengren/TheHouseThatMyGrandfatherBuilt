using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUpdater : MonoBehaviour
{
	private void Start()
	{
		foreach (ObjectState objectState in FindObjectsOfType<ObjectState>(true))
		{
			objectState.SetState();
		}
	}
	//Leah
	public void ActivateLeahStateEntering()
	{
		GlobalSceneData.leahState = GlobalSceneData.LeahState.Entering;
	}
	public void ActivateLeahStateBuilding()
	{
		GlobalSceneData.leahState = GlobalSceneData.LeahState.Building;
	}
	public void ActivateLeahStateDone()
	{
		if (GlobalSceneData.porchFixingState == GlobalSceneData.PorchFixingState.Fixed &&
			GlobalSceneData.windowsFixingState == GlobalSceneData.WindowsFixingState.Fixed &&
			GlobalSceneData.railingFixingState == GlobalSceneData.RailingFixingState.Fixed)
		{
			GlobalSceneData.leahState = GlobalSceneData.LeahState.Done;
		}
	}
	//George
	public void ActivateGeorgeStatePorch()
	{
		GlobalSceneData.georgeState = GlobalSceneData.GeorgeState.Porch;
	}
	public void ActivateGeorgeStateWindows()
	{
		GlobalSceneData.georgeState = GlobalSceneData.GeorgeState.Windows;
	}
	public void ActivateGeorgeStateRailing()
	{
		GlobalSceneData.georgeState = GlobalSceneData.GeorgeState.Railing;
	}
	//Porch
	public void ActivatePorchFixingStateBroken()
	{
		GlobalSceneData.porchFixingState = GlobalSceneData.PorchFixingState.Broken;
	}
	public void ActivatePorchFixingStateFixing()
	{
		GlobalSceneData.porchFixingState = GlobalSceneData.PorchFixingState.Fixing;
	}
	public void ActivatePorchFixingStateFixed()
	{
		GlobalSceneData.porchFixingState = GlobalSceneData.PorchFixingState.Fixed;
	}
	public void ActivatePorchStyleNone()
	{
		GlobalSceneData.porchStyle = GlobalSceneData.PorchStyle.None;
	}
	public void ActivatePorchStyleFlat()
	{
		GlobalSceneData.porchStyle = GlobalSceneData.PorchStyle.Flat;
	}
	public void ActivatePorchStyleSlanted()
	{
		GlobalSceneData.porchStyle = GlobalSceneData.PorchStyle.Slanted;
	}
	//Windows
	public void ActivateWindowsFixingStateBroken()
	{
		GlobalSceneData.windowsFixingState = GlobalSceneData.WindowsFixingState.Broken;
	}
	public void ActivateWindowsFixingStateFixing()
	{
		GlobalSceneData.windowsFixingState = GlobalSceneData.WindowsFixingState.Fixing;
	}
	public void ActivateWindowsFixingStateFixed()
	{
		GlobalSceneData.windowsFixingState = GlobalSceneData.WindowsFixingState.Fixed;
	}
	public void ActivateWindowsStyleNone()
	{
		GlobalSceneData.windowsStyle = GlobalSceneData.WindowsStyle.None;
	}
	public void ActivateWindowsStyleRibbed()
	{
		GlobalSceneData.windowsStyle = GlobalSceneData.WindowsStyle.Ribbed;
	}
	public void ActivateWindowsStyleSolid()
	{
		GlobalSceneData.windowsStyle = GlobalSceneData.WindowsStyle.Solid;
	}
	//Railing
	public void ActivateRailingFixingStateBroken()
	{
		GlobalSceneData.railingFixingState = GlobalSceneData.RailingFixingState.Broken;
	}
	public void ActivateRailingFixingStateFixing()
	{
		GlobalSceneData.railingFixingState = GlobalSceneData.RailingFixingState.Fixing;
	}
	public void ActivateRailingFixingStateFixed()
	{
		GlobalSceneData.railingFixingState = GlobalSceneData.RailingFixingState.Fixed;
	}
	public void ActivateRailingStyleNone()
	{
		GlobalSceneData.railingStyle = GlobalSceneData.RailingStyle.None;
	}
	public void ActivateRailingStyleFlatTop()
	{
		GlobalSceneData.railingStyle = GlobalSceneData.RailingStyle.FlatTop;
	}
	public void ActivateRailingStylePillars()
	{
		GlobalSceneData.railingStyle = GlobalSceneData.RailingStyle.Pillars;
	}
}
