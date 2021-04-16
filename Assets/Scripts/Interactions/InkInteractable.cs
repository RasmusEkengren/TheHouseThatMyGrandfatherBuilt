using UnityEngine;

public class InkInteractable : Interactable
{
	[SerializeField] private TextAsset StoryJson = null;
	public override void Interact(GameObject player)
	{
<<<<<<< HEAD
		player.SendMessage("StartStory", StoryJson, SendMessageOptions.DontRequireReceiver);
		interactIcon.SetActive(false);
=======
		//Super hacky fix, fix this into something good later
		if (interactIcon.activeSelf == true)
		{
			player.SendMessage("StartStory", StoryJson, SendMessageOptions.DontRequireReceiver);
			FMODUnity.RuntimeManager.PlayOneShot(interactSound);
			interactIcon.SetActive(false);
		}
>>>>>>> Max
	}
}
