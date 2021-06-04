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

    public void EmoteThink() { georgeAnimator.Play("Think"); }
    public void EmoteProud() { georgeAnimator.Play("Proud"); }
    public void EmoteDream() { georgeAnimator.Play("Dream"); }
    public void EmoteSigh() { georgeAnimator.Play("Sigh"); }
}
