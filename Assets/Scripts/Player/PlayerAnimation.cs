using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private PlayerMovement playerMovement = null;

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
}
