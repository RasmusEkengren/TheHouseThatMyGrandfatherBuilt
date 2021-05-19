using UnityEngine;

[CreateAssetMenu(fileName = "Default_Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class GameSettings : ScriptableObject
{
	public string Version = "0.2.05.19.1";
	public float TextPrintLength = 0.05f;
	public float AudioLevel;
}
