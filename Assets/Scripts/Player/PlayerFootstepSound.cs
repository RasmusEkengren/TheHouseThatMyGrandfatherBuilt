using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerFootstepSound : MonoBehaviour
{
    // Minor bug: The sound doesnt update flawlessly on every single step.
    // Tried a list of ints to stack the collisions. But that doesnt work out well in practice
    [SerializeField] [FMODUnity.EventRef] private string footstepSound = null;
    [SerializeField] [Range(0, 2)] private int soundParameter = 0;
    private EventInstance footstepInstance;

    [HideInInspector] public Collision currentCollision = null;

    private List<int> collisions = new List<int>();

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
        //UpdateCollision();

        footstepInstance.setParameterByName("Surface", soundParameter);
        footstepInstance.start();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Grass")
        {
            Debug.Log("Player collided with: Grass");
            soundParameter = 0;
            //collisions.Add(0);
        }
        if (collision.gameObject.tag == "Mud")
        {
            Debug.Log("Player collided with: Mud/Path");
            soundParameter = 1;
            //collisions.Add(1);
        }
        if (collision.gameObject.tag == "Wood")
        {
            Debug.Log("Player collided with: Wood");
            soundParameter = 2;
            //collisions.Add(2);
        }
    }

    //private void UpdateCollision()
    //{
    //    soundParameter = collisions[0];
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Grass" || collision.gameObject.tag == "Mud" || collision.gameObject.tag == "Wood")
    //    {
    //        collisions.RemoveAt(0);
    //    }
    //}
}
