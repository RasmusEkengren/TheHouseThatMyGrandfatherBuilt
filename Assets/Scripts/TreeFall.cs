using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    [SerializeField] [FMODUnity.EventRef] string treeFall = null;
    public float timeUntilPlanks = 3f;
    private Rigidbody treeRigidbody = null;
    [SerializeField] public GameObject planksToSpawn = null;
    [SerializeField] public GameObject treeToDisable = null;
    public Vector3 forcePower = new Vector3(0f,0f,0f);
    private Animator treeAnimator = null;

	private void Start()
	{
        treeAnimator = GetComponent<Animator>();
		treeRigidbody = GetComponent<Rigidbody>();
	}

	public void FellTree()
	{
		StartCoroutine("TreeFallSequence");
        treeAnimator.SetTrigger("fell tree");
		this.GetComponentInParent<EventInteractable>().setIsTrigger(true);
	}

	IEnumerator TreeFallSequence()
	{
		FMODUnity.RuntimeManager.PlayOneShot(treeFall);
		treeRigidbody.isKinematic = false;

		treeRigidbody.AddForce(forcePower);

        yield return new WaitForSeconds(timeUntilPlanks);
        treeToDisable.SetActive(false);
        planksToSpawn.SetActive(true);

		yield return null;
	}
}
