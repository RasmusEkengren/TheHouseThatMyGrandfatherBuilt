using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsTutorial : MonoBehaviour
{
    [SerializeField] private Image W = null;
    [SerializeField] private Image A = null;
    [SerializeField] private Image S = null;
    [SerializeField] private Image D = null;

    [SerializeField] private Image Spacebar = null;

    private float alphaValue;
    private Color imageColor = Color.white;

    private void Start()
    {
        imageColor.a = 1f;

        W.gameObject.SetActive(false);
        A.gameObject.SetActive(false);
        S.gameObject.SetActive(false);
        D.gameObject.SetActive(false);

        Spacebar.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ShowMovementControls(true);
    }

    public void ShowMovementControls(bool show)
    {
        if (show == true)
        {
            W.gameObject.SetActive(true);
            A.gameObject.SetActive(true);
            S.gameObject.SetActive(true);
            D.gameObject.SetActive(true);
        }

        else if (show == false)
        {
            W.gameObject.SetActive(false);
            A.gameObject.SetActive(false);
            S.gameObject.SetActive(false);
            D.gameObject.SetActive(false);
        }
    }

    public void ShowInteractionControls()
    {
        Spacebar.gameObject.SetActive(true);
    }

    public void HideInteractionControls()
    {
        Spacebar.gameObject.SetActive(false);
    }

    private IEnumerator FadeInMovement()
    {
        if (alphaValue <= 1)
        {
            alphaValue += 0.2f * Time.deltaTime;
            imageColor.a = alphaValue;
            W.color = imageColor;
            A.color = imageColor;
            S.color = imageColor;
            D.color = imageColor;
        }
        else
        {
            yield return null;
        }
        yield return null;
    }
}
