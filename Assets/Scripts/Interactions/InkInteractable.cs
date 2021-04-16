using UnityEngine;

public class InkInteractable : Interactable
{
	[SerializeField] private TextAsset StoryJson = null;
	public override void Interact(GameObject player)
	{
		player.SendMessage("StartStory", StoryJson, SendMessageOptions.DontRequireReceiver);
		interactIcon.SetActive(false);
	}
}
