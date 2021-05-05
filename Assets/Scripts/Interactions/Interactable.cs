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
		mainCamera = Camera.main;
		//Check if has interacted before in data
		ParticleSystem.MainModule main = particle.main;
		ParticleSystem.ColorOverLifetimeModule colorModule = particle.colorOverLifetime;
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
			interactIcon.SetActive(true);
			interactIcon.transform.forward = mainCamera.transform.forward;
		}
	}
	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == playerTag)
		{
			interactIcon.SetActive(false);
		}
	}
	public virtual void Interact(GameObject player)
	{
		interactIcon.SetActive(false);
		hasInteracted = true;
		//Set to interacted with in data
		ParticleSystem.MainModule main = particle.main;
		ParticleSystem.ColorOverLifetimeModule colorModule = particle.colorOverLifetime;
		main.startColor = Color.white;
		colorModule.color = interactedColor;
		FMODUnity.RuntimeManager.PlayOneShot(interactSound);
	}
}