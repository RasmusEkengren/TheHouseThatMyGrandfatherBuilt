using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerFootstepSound : MonoBehaviour
{
    [SerializeField] [FMODUnity.EventRef] private string footstepSound = null;
    [SerializeField] private Collider footstepCollider = null;

    [SerializeField] [Range(0, 3)] private int soundParameter = 0;

    //private float _interval;
    //[SerializeField] public float footstepInterval { get { return _interval; } [SerializeField] private set { _interval = value; } }
    [SerializeField] public float footstepInterval = 0.5f;

    private EventInstance footstepInstance;
    private void Start()
    {
        footstepCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        footstepInstance = RuntimeManager.CreateInstance(footstepSound);
        footstepInstance.setParameterByName(footstepSound.ToString(), soundParameter);
    }

    private void OnDisable()
    {
        footstepInstance.release();
    }

    public void PlayFootstep()
    {
        footstepInstance.start();
    }

    // Player moves, colliding with  different object
    // If object is ground. Get the tag of that ground
    // Update current parameter with the sound equivalent to that tag
    // When PlayerMovement calls PlayFootstep(), play that sound

    // Get current collided tag
    // Change sound based on that
}
