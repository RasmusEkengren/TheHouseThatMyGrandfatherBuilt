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
	public void ActivatePorchFixed()
	{
		GlobalSceneData.mg_porchFixed = true;
	}
	public void ActivatePorchFixing()
	{
		GlobalSceneData.mg_porchFixing = true;
	}
	public void ActivateWindowsFixed()
	{
		GlobalSceneData.mg_windowsFixed = true;
	}
	public void ActivateWindowsFixing()
	{
		GlobalSceneData.mg_windowsFixing = true;
	}
	public void ActivateRailingFixed()
	{
		GlobalSceneData.mg_railingFixed = true;
	}
	public void ActivateRailingFixing()
	{
		GlobalSceneData.mg_railingFixing = true;
	}
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
		if (GlobalSceneData.mg_porchFixed && GlobalSceneData.mg_railingFixed && GlobalSceneData.mg_windowsFixed)
		{
			GlobalSceneData.leahState = GlobalSceneData.LeahState.Done;
		}
	}
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
	public void ActivatePorchStateBroken()
	{
		GlobalSceneData.porchState = GlobalSceneData.PorchState.Broken;
	}
	public void ActivatePorchStateFlat()
	{
		GlobalSceneData.porchState = GlobalSceneData.PorchState.Flat;
	}
	public void ActivatePorchStateSlanted()
	{
		GlobalSceneData.porchState = GlobalSceneData.PorchState.Slanted;
	}
	public void ActivateWindowsStateBroken()
	{
		GlobalSceneData.windowsState = GlobalSceneData.WindowsState.Broken;
	}
	public void ActivateWindowsStateRibbed()
	{
		GlobalSceneData.windowsState = GlobalSceneData.WindowsState.Ribbed;
	}
	public void ActivateWindowsStateSolid()
	{
		GlobalSceneData.windowsState = GlobalSceneData.WindowsState.Solid;
	}
	public void ActivateRailingStateBroken()
	{
		GlobalSceneData.railingState = GlobalSceneData.RailingState.Broken;
	}
	public void ActivateRailingStateFlatTop()
	{
		GlobalSceneData.railingState = GlobalSceneData.RailingState.FlatTop;
	}
	public void ActivateRailingStatePillars()
	{
		GlobalSceneData.railingState = GlobalSceneData.RailingState.Pillars;
	}
}
