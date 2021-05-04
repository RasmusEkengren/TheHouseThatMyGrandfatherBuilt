using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerFootstepSound : MonoBehaviour
{
    [SerializeField] [FMODUnity.EventRef] private string footstepSound = null;
    [SerializeField] [Range(0, 2)] private int soundParameter = 0;
    private EventInstance footstepInstance;

    [HideInInspector] public GameObject currentCollision = null;

    private void Start()
    {
    }

    private void OnEnable()
    {
        footstepInstance = RuntimeManager.CreateInstance(footstepSound);
        footstepInstance.setParameterByName("Surface", soundParameter);
    }

    private void OnDisable()
    {
        footstepInstance.release();
    }

    public void PlayFootstep()
    {
        GetParameter();
        footstepInstance.setParameterByName("Surface", soundParameter);
        footstepInstance.start();
    }

    public void GetParameter()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Grass")
        {
            Debug.Log("Player collided with: Grass");
            currentCollision = collision.gameObject;
            soundParameter = 0;
        }
        if (collision.gameObject.tag == "Mud")
        {
            Debug.Log("Player collided with: Mud/Path");
            currentCollision = collision.gameObject;
            soundParameter = 1;
        }
        if (collision.gameObject.tag == "Wood")
        {
            Debug.Log("Player collided with: Wood");
            currentCollision = collision.gameObject;
            soundParameter = 2;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
       
    }

    // Player moves, colliding with  different object
    // If object is ground. Get the tag of that ground
    // When PlayerMovement calls PlayFootstep(), play that sound

    // Get current collided tag
    // Change sound based on that
}
