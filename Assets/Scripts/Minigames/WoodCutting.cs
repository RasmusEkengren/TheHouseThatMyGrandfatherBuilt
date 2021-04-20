using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class WoodCutting : MonoBehaviour
{
	[SerializeField] private GameObject circle = null;
	[SerializeField] private GameObject HitIndicator = null;
	[SerializeField] private float startSize = 5f;
	[SerializeField] private float startHealth = 3;
	[SerializeField] private float gameSpeed = 1f;
	[SerializeField] private float tolerance = 0.2f;
	[SerializeField] [FMODUnity.EventRef] protected string cutSound = null;
	[SerializeField] private UnityEvent finishEvent = null;
	private float health = 0;
	private bool isGameActive = false;
	void Start()
	{
		circle.gameObject.SetActive(false);
		HitIndicator.gameObject.SetActive(false);
	}
	void Update()
	{
		if (isGameActive)
		{
			if (health <= 0)
			{
				isGameActive = false;
				finishEvent.Invoke();
				circle.gameObject.SetActive(false);
				HitIndicator.gameObject.SetActive(false);
			}
			if (circle.transform.localScale.x > 0)
			{
				circle.transform.localScale -= Vector3.one * gameSpeed * Time.deltaTime;
			}
			else
			{
				ResetIndicator();
			}
		}
	}
	public void StartGame()
	{
		isGameActive = true;
		circle.gameObject.SetActive(true);
		HitIndicator.gameObject.SetActive(true);
		health = startHealth;
		ResetIndicator();
	}
	private void ResetIndicator()
	{
		circle.transform.localScale = Vector3.one * startSize;
		HitIndicator.transform.localScale = Vector3.one;
	}
	public void Hit(InputAction.CallbackContext value)
	{
		if (!gameObject.scene.IsValid()) return;
		if (value.performed && isGameActive)
		{
			FMODUnity.RuntimeManager.PlayOneShot(cutSound);
			float strength = circle.transform.localScale.x;
			float target = HitIndicator.transform.localScale.x;
			if (strength < target + tolerance && strength > target - tolerance) health -= 1;
			Debug.Log(strength + ", " + target);
			ResetIndicator();
		}
	}
}