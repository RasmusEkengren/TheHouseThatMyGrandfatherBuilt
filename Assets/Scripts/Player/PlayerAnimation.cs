using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private PlayerMovement playerMovement = null;
    private float currentLayerWeight = 0;
    private int layerIndex = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        // If player is walking, play walking animation
        if (playerMovement.isWalking)
        {
            animator.SetBool("walking", true);
        }
        // Else, go back to idle
        else
        {
            animator.SetBool("walking", false);
        }
    }

    public void StartCarryAnimation()
    {
        layerIndex = animator.GetLayerIndex("Carry");
        ChangeLayerWeight(layerIndex, 1);
    }
    public void StopCarryAnimation()
    {
        layerIndex = animator.GetLayerIndex("Carry");
        ChangeLayerWeight(layerIndex, 0);
    }

    private IEnumerator ChangeLayerWeight(int layerIndex, int weightTarget)
    {
        if (weightTarget > 0)
        {
            currentLayerWeight = animator.GetLayerWeight(layerIndex);
            currentLayerWeight += 0.5f * Time.deltaTime;
            animator.SetLayerWeight(layerIndex, currentLayerWeight);
            if (currentLayerWeight >= 1)
            {
                yield return null;
            }
            // Go to 1
        }
        if (weightTarget <= 0)
        {
            // Go to 0
        }

        yield return null;
    }

}
