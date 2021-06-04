using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GeorgeEmotes : MonoBehaviour
{
    private Animator georgeAnimator = null;

    private void Start()
    {
        georgeAnimator = GetComponent<Animator>();
    }
}
