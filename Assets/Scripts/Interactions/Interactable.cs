using UnityEngine;

public class Interactable : MonoBehaviour
{
	private Camera mainCamera = null;
	[SerializeField] protected GameObject interactIcon = null;
	[SerializeField] protected ParticleSystem particle = null;
	[SerializeField] private string playerTag = "Player";
	[SerializeField] private Gradient normalColor = null;
	[SerializeField] private Gradient interactedColor = null;
	[SerializeField] [FMODUnity.EventRef] protected string interactSound = null;
	private bool hasInteracted = false;
	void Start()
	{
		this.gameObject.GetComponent<UniqueID>().CheckID();
		mainCamera = Camera.main;
		//Check if has interacted before in data
		ParticleSystem.MainModule main = particle.main;
		ParticleSystem.ColorOverLifetimeModule colorModule = particle.colorOverLifetime;
		if (GlobalSceneData.FindInteractedState(this.gameObject.GetComponent<UniqueID>().ID)) hasInteracted = true;
		if (hasInteracted)
		{
			main.startColor = Color.white;
			colorModule.color = interactedColor;
		}
		else colorModule.color = normalColor;
	}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == playerTag)
		{
			GlobalSceneData.lastInteraction = this;
			interactIcon.SetActive(true);
			ControlsTutorial.ShowInteractionControls(true);
			if (mainCamera != null && interactIcon != null)
			{
				interactIcon.transform.forward = mainCamera.transform.forward;
			}
		}
	}
	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == playerTag)
		{
			if (GlobalSceneData.lastInteraction == this) { GlobalSceneData.lastInteraction = null; }
			interactIcon.SetActive(false);
			ControlsTutorial.ShowInteractionControls(false);
		}
	}
	public virtual void Interact(GameObject player)
	{
		ControlsTutorial.ShowInteractionControls(false);
		interactIcon.SetActive(false);
		hasInteracted = true;
		GlobalSceneData.interactedObjectIDs.Add(this.gameObject.GetComponent<UniqueID>().ID);
		//Set to interacted with in data
		ParticleSystem.MainModule main = particle.main;
		ParticleSystem.ColorOverLifetimeModule colorModule = particle.colorOverLifetime;
		main.startColor = Color.white;
		colorModule.color = interactedColor;
		FMODUnity.RuntimeManager.PlayOneShot(interactSound);
	}
	public void ResetInteraction()
	{
		hasInteracted = false;
		GlobalSceneData.interactedObjectIDs.Remove(this.gameObject.GetComponent<UniqueID>().ID);
		ParticleSystem.MainModule main = particle.main;
		ParticleSystem.ColorOverLifetimeModule colorModule = particle.colorOverLifetime;
		main.startColor = new Color(255, 243, 0, 255);
		colorModule.color = normalColor;
	}
}