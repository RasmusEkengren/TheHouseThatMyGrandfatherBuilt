using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeahEmotes : MonoBehaviour
{
    private Animator leahAnimator = null;

    private void Start()
    {
        leahAnimator = GetComponent<Animator>();
    }

    public void EmoteThink() { leahAnimator.Play("Think"); }
    public void EmoteProud() { leahAnimator.Play("Proud"); }
    public void EmoteDream() { leahAnimator.Play("Dream"); }
    public void EmoteSigh() { leahAnimator.Play("Sigh"); }
}
