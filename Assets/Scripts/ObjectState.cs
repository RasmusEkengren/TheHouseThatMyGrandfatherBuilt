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
        [Header("George States")]
		public activeStates OnGeorgeStatePorch = activeStates.NONE;
		public activeStates OnGeorgeStateWindows = activeStates.NONE;
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
			if (GlobalSceneData.porchFixed == GlobalSceneData.PorchFixed.Flat)
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
			else if (GlobalSceneData.porchFixed == GlobalSceneData.PorchFixed.Slanted)
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
	}
}
