using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WoodCutting : MonoBehaviour
{
    [SerializeField] private GameObject circle = null;
    [SerializeField] private GameObject HitIndicator = null;
    [SerializeField] private float startSize = 5f;
    [SerializeField] private float startHealth = 3;
    [SerializeField] private float gameSpeed = 1f;
    [SerializeField] private float tolerance = 0.2f;
    [SerializeField] [FMODUnity.EventRef] protected string cutSound = null;
    [SerializeField] [FMODUnity.EventRef] protected string missSound = null;
    [SerializeField] private UnityEvent finishEvent = null;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hitColor = Color.green;
    [SerializeField] private Color missColor = Color.red;
    private float health = 0;
    private bool isGameActive = false;
    private float waitTimer = 0.2f;
    private Image circleImage = null;

    public Animator animator = null;

    private bool IsLeahScene = false;

    void Start()
    {
        circle.gameObject.SetActive(false);
        HitIndicator.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Leah")
        {
            IsLeahScene = true;
        }

    }
    void Update()
    {
        if (isGameActive)
        {
            if (waitTimer > 0f)
            {
                waitTimer -= Time.deltaTime;
            }
            else
            {
                circleImage.color = normalColor;
                if (health <= 0)
                {
                    isGameActive = false;
                    finishEvent.Invoke();
                    circle.gameObject.SetActive(false);
                    HitIndicator.gameObject.SetActive(false);
                    if (!IsLeahScene) animator.Play("Idle");
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
    }
    public void StartGame()
    {

        if (!IsLeahScene) animator.Play("Axe Idle");
        isGameActive = true;
        circle.gameObject.SetActive(true);
        circleImage = circle.GetComponent<Image>();
        normalColor = circleImage.color;
        HitIndicator.gameObject.SetActive(true);
        health = startHealth;
        ResetIndicator();
    }
    private void ResetIndicator()
    {
        circle.transform.localScale = Vector3.one * startSize;
        HitIndicator.transform.localScale = Vector3.one;
        waitTimer = 0.2f;
    }
    public void Hit(InputAction.CallbackContext value)
    {
        if (!gameObject.scene.IsValid()) return;
        if (value.performed && isGameActive && waitTimer < 0f)
        {

            float strength = circle.transform.localScale.x;
            float target = HitIndicator.transform.localScale.x;
            if (strength < target + tolerance && strength > target - tolerance)
            {
                if (!IsLeahScene) animator.Play("Axe Hit");
                circleImage.color = hitColor;
                health -= 1;
                FMODUnity.RuntimeManager.PlayOneShot(cutSound);
            }
            else
            {
                if (!IsLeahScene) animator.Play("Axe Miss");
                circleImage.color = missColor;
                FMODUnity.RuntimeManager.PlayOneShot(missSound);
            }
            ResetIndicator();
        }
    }
}