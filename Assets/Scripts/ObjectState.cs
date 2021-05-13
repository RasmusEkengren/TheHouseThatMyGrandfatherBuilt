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
		public activeStates OnPorchFlat = activeStates.NONE;
		public activeStates OnPorchSlanted = activeStates.NONE;
		public activeStates OnPorchBroken = activeStates.NONE;
        [Header("Windows")]
		public activeStates OnWindowsFixed = activeStates.NONE;
		public activeStates OnWindowsBroken = activeStates.NONE;
        [Header("Fence")]
        public activeStates OnFenceFixed = activeStates.NONE;
		public activeStates OnFenceBroken = activeStates.NONE;
        [Header("Leah")]
        public activeStates OnLeahEntering = activeStates.NONE;
		public activeStates OnLeahBuilding = activeStates.NONE;
		public activeStates OnLeahDone = activeStates.NONE;
        [Header("George")]
        public activeStates OnGeorgeStatePorch = activeStates.NONE;
		public activeStates OnGeorgeStateWindows = activeStates.NONE;
		public activeStates OnGeorgeStateFence = activeStates.NONE;
	}
	[SerializeField] private BoolStates boolStates;

	public void SetState()
	{
		if (GlobalSceneData.mg_porchFixed)
		{
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
			if (GlobalSceneData.porchState == GlobalSceneData.PorchState.Flat)
			{
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
			}
			else if (GlobalSceneData.porchState == GlobalSceneData.PorchState.Slanted)
			{
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
			}
		}
		else
		{
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
		}
		if (GlobalSceneData.mg_windowsFixed)
		{
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
		}
		else
		{
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
		}
		if (GlobalSceneData.mg_fenceFixed)
		{
			switch (boolStates.OnFenceFixed)
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
			switch (boolStates.OnFenceBroken)
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
		else if (GlobalSceneData.georgeState == GlobalSceneData.GeorgeState.Fence)
		{
			switch (boolStates.OnGeorgeStateFence)
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
