using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DelayedButtonEvents : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [SerializeField] private UnityEvent OnClick = null;

    private Button button = null;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(InvokeOnClick);
    }

    private void InvokeOnClick()
    {
        StartCoroutine("DelayInvoke");
    }

    private IEnumerator DelayInvoke()
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Delayed button click");
        OnClick.Invoke();
        yield return null;
    }
}
