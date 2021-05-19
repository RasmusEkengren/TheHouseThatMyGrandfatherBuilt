using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsTutorial : MonoBehaviour
{
    [SerializeField] private Image WImage = null;
    [SerializeField] private Image AImage = null;
    [SerializeField] private Image SImage = null;
    [SerializeField] private Image DImage = null;

    private static Image W = null;
    private static Image A = null;
    private static Image S = null;
    private static Image D = null;

    [SerializeField] private Image SpacebarImage = null;
    private static Image Spacebar = null;

    private float alphaValue;
    private Color imageColor = Color.white;

    private void Start()
    {
        W = WImage;
        A = AImage;
        S = SImage;
        D = DImage;
        Spacebar = SpacebarImage;

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

    public static void ShowMovementControls(bool show)
    {
        W.gameObject.SetActive(show);
        A.gameObject.SetActive(show);
        S.gameObject.SetActive(show);
        D.gameObject.SetActive(show);
    }

    public static void ShowInteractionControls(bool show)
    {
        Spacebar.gameObject.SetActive(show);
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
