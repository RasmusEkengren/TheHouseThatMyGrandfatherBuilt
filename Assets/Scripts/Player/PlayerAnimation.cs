using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private PlayerMovement playerMovement = null;
    private float currentLayerWeight = 0;

    private int layerIndex = 0; // Keep eye on this, might cause issues when used by multiple methods
    private int weightTarget = 0;

    private PlankBalancing plank = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        currentLayerWeight = animator.GetLayerWeight(layerIndex);
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

    public void StartAxeHoldingAnimation()
    {
        layerIndex = animator.GetLayerIndex("Carrying Axe");
        animator.SetLayerWeight(layerIndex, 1);
        Debug.Log("Starting axe holding animation");
    }

    public void StopAxeHoldingAnimation()
    {
        layerIndex = animator.GetLayerIndex("Carrying Axe");
        animator.SetLayerWeight(layerIndex, 0);
        Debug.Log("Stopping axe holding animation");
    }

    public void StartCarryAnimation()
    {
        layerIndex = animator.GetLayerIndex("Carry");
        animator.SetLayerWeight(layerIndex, 1);
        layerIndex = animator.GetLayerIndex("Falling");
        animator.SetLayerWeight(layerIndex, 1);
        // weightTarget = 1;
        //StartCoroutine("ChangeLayerWeight");
        Debug.Log("Starting carry animation");
    }
    public void StopCarryAnimation()
    {
        layerIndex = animator.GetLayerIndex("Carry");
        animator.SetLayerWeight(layerIndex, 0);
        layerIndex = animator.GetLayerIndex("Falling");
        animator.SetLayerWeight(layerIndex, 0);
        // weightTarget = 0;
        // StartCoroutine("ChangeLayerWeight");
        Debug.Log("Stopping carry animation");
    }

    public void ResumeWalking()
    {
        playerMovement.ResumeWalking();
        if (FindObjectOfType<PlankBalancing>())
        {
            plank = FindObjectOfType<PlankBalancing>();
            plank.ResetGame();
        }
    }

    private IEnumerator ChangeLayerWeight()
    {
        if (weightTarget > 0)
        {
            currentLayerWeight += 0.5f * Time.deltaTime;
            animator.SetLayerWeight(layerIndex, currentLayerWeight);
            if (currentLayerWeight >= 1)
            {
                yield return null;
            }
            Debug.Log("Yes, we are carrying..." + weightTarget);
            // Go to 1
        }
        if (weightTarget <= 0)
        {
            // Go to 0
        }

        yield return null;
    }

}
