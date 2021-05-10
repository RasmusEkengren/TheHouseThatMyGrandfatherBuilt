using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : ConditionalInteraction
{
	[SerializeField] private AxeTracker player;
	[SerializeField] [FMODUnity.EventRef] string treeFall = null;
    public float treeFallDuration = 3f;
    [SerializeField] public Rigidbody treeRigidbody = null;
    [SerializeField] public GameObject tree = null;
    [SerializeField] public GameObject planks = null;
    public ParticleSystem leavesParticles = null;

	public void CheckAxe() 
	{
		CheckCondition(player.hasAxe);
	}

	public void Play()
	{
        StartCoroutine("TreeFallSequence");
	}

    IEnumerator TreeFallSequence()
    {
        FMODUnity.RuntimeManager.PlayOneShot(treeFall);
        treeRigidbody.isKinematic = false;

        // Leaves particle effects

        yield return new WaitForSeconds(treeFallDuration);
        tree.SetActive(false);
        planks.SetActive(true);
        // Play particle effects and transform tree into planks

        yield return null;
    }

    public void EmitLeaves()
    {
        leavesParticles.Play();
    }
}