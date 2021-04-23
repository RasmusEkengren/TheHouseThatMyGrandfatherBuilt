using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private RectTransform topBorder;
    [SerializeField] private RectTransform bottomBorder;
    [SerializeField] private UnityEvent startEvents;
    void Start()
    {
        if (!GameController.introDone)
        {
            startEvents.Invoke();
        }
    }
    public void StartBorder()
    {
        topBorder.gameObject.SetActive(true);
        bottomBorder.gameObject.SetActive(true);
    }
    public void EndBorder()
    {
        topBorder.gameObject.SetActive(false);
        bottomBorder.gameObject.SetActive(false);
    }
}
