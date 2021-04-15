using UnityEngine;

public class WoodCutting : MonoBehaviour
{
	[SerializeField] private GameObject circle = null;
	[SerializeField] private GameObject HitIndicator = null;
	[SerializeField] private float startSize = 5f;
	[SerializeField] private float startHealth = 3;
	[SerializeField] private float gameSpeed = 1f;
	[SerializeField] private float tolerance = 0.2f;
	private float health = 0;
	void OnInteract()
	{
		Hit();
	}
	void OnClick()
	{
		Hit();
	}
	void Start()
	{
		circle.gameObject.SetActive(false);
		HitIndicator.gameObject.SetActive(false);
	}
	void Update()
	{
		if (health <= 0)
		{
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
	public void StartGame()
	{
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
	private void Hit()
	{
		float strength = circle.transform.localScale.x;
		float target = HitIndicator.transform.localScale.x;
		if (strength < target + tolerance && strength > target - tolerance) health -= 1;
		Debug.Log(strength + ", " + target);
		ResetIndicator();
	}
}