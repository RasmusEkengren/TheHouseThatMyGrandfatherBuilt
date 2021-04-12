using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent trigger = null;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision trigger", gameObject);
        trigger.Invoke();
    }
}
