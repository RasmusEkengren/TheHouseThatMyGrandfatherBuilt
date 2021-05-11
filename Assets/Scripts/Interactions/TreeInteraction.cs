using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : ConditionalInteraction
{
	[SerializeField] private AxeTracker player;
	[SerializeField] [FMODUnity.EventRef] string treeFall = null;
    public float treeFallDuration = 6f;
    [SerializeField] public Rigidbody treeRigidbody = null;
    [SerializeField] public GameObject tree = null;
    [SerializeField] public GameObject planks = null;
    [SerializeField] public Collider InteractCollider = null;


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
        InteractCollider.enabled = false;
        FMODUnity.RuntimeManager.PlayOneShot(treeFall);
        treeRigidbody.isKinematic = false;

        yield return new WaitForSeconds(treeFallDuration);
        tree.SetActive(false);
        planks.SetActive(true);

        yield return null;
    }
}