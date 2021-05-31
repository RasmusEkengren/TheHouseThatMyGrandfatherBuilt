using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    [SerializeField] [FMODUnity.EventRef] string treeFall = null;
    public float timeUntilPlanks = 3f;
    [SerializeField] public GameObject planksToSpawn = null;
    [SerializeField] public GameObject treeToDisable = null;
    public Vector3 forcePower = new Vector3(0f,0f,0f);
    private Animator treeAnimator = null;
    private Collider treeCollider = null;

	private void Start()
	{
        treeAnimator = GetComponent<Animator>();
        treeCollider = GetComponent<Collider>();
	}

	public void FellTree()
	{
		StartCoroutine("TreeFallSequence");
        treeAnimator.SetTrigger("fell tree");
		GetComponentInParent<EventInteractable>().setIsTrigger(true);
	}

	IEnumerator TreeFallSequence()
	{
		FMODUnity.RuntimeManager.PlayOneShot(treeFall);
        treeCollider.enabled = false;
		//treeRigidbody.isKinematic = false;

		//treeRigidbody.AddForce(forcePower);

        yield return new WaitForSeconds(timeUntilPlanks);
        treeToDisable.SetActive(false);
        planksToSpawn.SetActive(true);

		yield return null;
	}
}
