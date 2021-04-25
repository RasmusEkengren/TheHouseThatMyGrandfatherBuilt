using UnityEngine;

[CreateAssetMenu(fileName = "Default_Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class GameSettings : ScriptableObject
{
	public string Version = "0.1.04.16.1";
	public int TextSpeed = 6;
	public float AudioLevel;
}
