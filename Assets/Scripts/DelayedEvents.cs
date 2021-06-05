using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvents : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [Header("Run these events using InvokeEvents() through another event")]
    [SerializeField] private UnityEvent events = null;

    public void InvokeEvents()
    {
        StartCoroutine("DelayedInvocation");
    }
    IEnumerator DelayedInvocation()
    {
        yield return new WaitForSeconds(delay);
        events.Invoke();
        yield return null;
    }
}
