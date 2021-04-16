using UnityEngine;

[CreateAssetMenu(fileName = "Default_Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class GameSettings : ScriptableObject
{
	public int TextSpeed;
	public float AudioLevel;
}
