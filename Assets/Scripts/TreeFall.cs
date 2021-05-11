using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    [SerializeField] private AxeTracker player;
    [SerializeField] [FMODUnity.EventRef] string treeFall = null;
    public float treeFallDuration = 6f;
    [SerializeField] private Rigidbody treeRigidbody = null;
    [SerializeField] public GameObject tree = null;
    [SerializeField] public GameObject planks = null;
    [SerializeField] public Collider InteractCollider = null;
    private bool done = false;

    private void Start()
    {
        treeRigidbody = GetComponent<Rigidbody>();
    }

    public void FellTree()
    {
        StartCoroutine("TreeFallSequence");
    }

    IEnumerator TreeFallSequence()
    {
        InteractCollider.enabled = false;
        FMODUnity.RuntimeManager.PlayOneShot(treeFall);
        treeRigidbody.isKinematic = false;

        treeRigidbody.AddForceAtPosition(Vector3.zero, Vector3.zero, ForceMode.Impulse);

        yield return new WaitForSeconds(treeFallDuration);
        tree.SetActive(false);
        planks.SetActive(true);

        yield return null;
    }
}
