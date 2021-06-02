using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
	[Serializable]
	private class BoolStates
	{
		public enum activeStates { ON, OFF, NONE }
		[Header("Porch")]
		public activeStates OnPorchFixed = activeStates.NONE;
		public activeStates OnPorchFixing = activeStates.NONE;
		public activeStates OnPorchFlat = activeStates.NONE;
		public activeStates OnPorchSlanted = activeStates.NONE;
		public activeStates OnPorchBroken = activeStates.NONE;
		[Header("Windows")]
		public activeStates OnWindowsFixed = activeStates.NONE;
		public activeStates OnWindowsFixing = activeStates.NONE;
		public activeStates OnWindowsRibbed = activeStates.NONE;
		public activeStates OnWindowsSolid = activeStates.NONE;
		public activeStates OnWindowsBroken = activeStates.NONE;
		[Header("Railing")]
		public activeStates OnRailingFixed = activeStates.NONE;
		public activeStates OnRailingFixing = activeStates.NONE;
		public activeStates OnRailingFlatTop = activeStates.NONE;
		public activeStates OnRailingPillars = activeStates.NONE;
		public activeStates OnRailingBroken = activeStates.NONE;
		[Header("Leah")]
		public activeStates OnLeahEntering = activeStates.NONE;
		public activeStates OnLeahBuilding = activeStates.NONE;
		public activeStates OnLeahDone = activeStates.NONE;
		[Header("George")]
		public activeStates OnGeorgeStatePorch = activeStates.NONE;
		public activeStates OnGeorgeStateWindows = activeStates.NONE;
		public activeStates OnGeorgeStateRailing = activeStates.NONE;
		[Header("Pickups")]
		public activeStates OnFramePickUpTrue = activeStates.NONE;
		public activeStates OnFramePickUpFalse = activeStates.NONE;
	}
	[SerializeField] private BoolStates boolStates;

	public void SetState()
	{
		//PorchFixingState
		switch (GlobalSceneData.porchFixingState)
		{
			case GlobalSceneData.PorchFixingState.Broken:
				switch (boolStates.OnPorchBroken)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.PorchFixingState.Fixing:
				switch (boolStates.OnPorchFixing)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.PorchFixingState.Fixed:
				switch (boolStates.OnPorchFixed)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			default:
				break;
		}
		//PorchStyle
		switch (GlobalSceneData.porchStyle)
		{
			case GlobalSceneData.PorchStyle.Flat:
				switch (boolStates.OnPorchFlat)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.PorchStyle.Slanted:
				switch (boolStates.OnPorchSlanted)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			default:
				break;
		}
		//WindowsFixingState
		switch (GlobalSceneData.windowsFixingState)
		{
			case GlobalSceneData.WindowsFixingState.Broken:
				switch (boolStates.OnWindowsBroken)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.WindowsFixingState.Fixing:
				switch (boolStates.OnWindowsFixing)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.WindowsFixingState.Fixed:
				switch (boolStates.OnWindowsFixed)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			default:
				break;
		}
		//WindowsStyle
		switch (GlobalSceneData.windowsStyle)
		{
			case GlobalSceneData.WindowsStyle.Ribbed:
				switch (boolStates.OnWindowsRibbed)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.WindowsStyle.Solid:
				switch (boolStates.OnWindowsSolid)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			default:
				break;
		}
		//RailingFixingState
		switch (GlobalSceneData.railingFixingState)
		{
			case GlobalSceneData.RailingFixingState.Broken:
				switch (boolStates.OnRailingBroken)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.RailingFixingState.Fixing:
				switch (boolStates.OnRailingFixing)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.RailingFixingState.Fixed:
				switch (boolStates.OnRailingFixed)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			default:
				break;
		}

		switch (GlobalSceneData.railingStyle)
		{
			case GlobalSceneData.RailingStyle.FlatTop:
				switch (boolStates.OnRailingFlatTop)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			case GlobalSceneData.RailingStyle.Pillars:
				switch (boolStates.OnRailingPillars)
				{
					case BoolStates.activeStates.ON:
						this.gameObject.SetActive(true);
						break;
					case BoolStates.activeStates.OFF:
						this.gameObject.SetActive(false);
						return;
					default:
						break;
				}
				break;
			default:
				break;
		}
		if (GlobalSceneData.leahState == GlobalSceneData.LeahState.Entering)
		{
			switch (boolStates.OnLeahEntering)
			{
				case BoolStates.activeStates.ON:
					this.gameObject.SetActive(true);
					break;
				case BoolStates.activeStates.OFF:
					this.gameObject.SetActive(false);
					return;
				default:
					break;
			}
		}
		else if (GlobalSceneData.leahState == GlobalSceneData.LeahState.Building)
		{
			switch (boolStates.OnLeahBuilding)
			{
				case BoolStates.activeStates.ON:
					this.gameObject.SetActive(true);
					break;
				case BoolStates.activeStates.OFF:
					this.gameObject.SetActive(false);
					return;
				default:
					break;
			}
		}
		else if (GlobalSceneData.leahState == GlobalSceneData.LeahState.Done)
		{
			switch (boolStates.OnLeahDone)
			{
				case BoolStates.activeStates.ON:
					this.gameObject.SetActive(true);
					break;
				case BoolStates.activeStates.OFF:
					this.gameObject.SetActive(false);
					return;
				default:
					break;
			}
		}
		if (GlobalSceneData.georgeState == GlobalSceneData.GeorgeState.Porch)
		{
			switch (boolStates.OnGeorgeStatePorch)
			{
				case BoolStates.activeStates.ON:
					this.gameObject.SetActive(true);
					break;
				case BoolStates.activeStates.OFF:
					this.gameObject.SetActive(false);
					return;
				default:
					break;
			}
		}
		else if (GlobalSceneData.georgeState == GlobalSceneData.GeorgeState.Windows)
		{
			switch (boolStates.OnGeorgeStateWindows)
			{
				case BoolStates.activeStates.ON:
					this.gameObject.SetActive(true);
					break;
				case BoolStates.activeStates.OFF:
					this.gameObject.SetActive(false);
					return;
				default:
					break;
			}
		}
		else if (GlobalSceneData.georgeState == GlobalSceneData.GeorgeState.Railing)
		{
			switch (boolStates.OnGeorgeStateRailing)
			{
				case BoolStates.activeStates.ON:
					this.gameObject.SetActive(true);
					break;
				case BoolStates.activeStates.OFF:
					this.gameObject.SetActive(false);
					return;
				default:
					break;
			}
		}
		if (GlobalSceneData.pickedUpPictureFrame)
		{
			switch (boolStates.OnFramePickUpTrue)
			{
				case BoolStates.activeStates.ON:
					this.gameObject.SetActive(true);
					break;
				case BoolStates.activeStates.OFF:
					this.gameObject.SetActive(false);
					return;
				default:
					break;
			}
		}
		else
		{
			switch (boolStates.OnFramePickUpFalse)
			{
				case BoolStates.activeStates.ON:
					this.gameObject.SetActive(true);
					break;
				case BoolStates.activeStates.OFF:
					this.gameObject.SetActive(false);
					return;
				default:
					break;
			}
		}
	}
}
