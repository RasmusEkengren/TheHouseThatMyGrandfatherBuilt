using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : ConditionalInteraction
{
    [SerializeField] private AxeTracker player;
    private bool allowInteraction = true;

    public override void CheckCondition(bool condition)
    {
        if (allowInteraction)
        {
            base.CheckCondition(condition);
        }
    }

    public void CheckAxe()
    {
        CheckCondition(player.hasAxe);
    }

    public void AllowInteraction(bool _bool)
    {
        allowInteraction = _bool;
    }
}