using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    [SerializeField] [FMODUnity.EventRef] string treeFall = null;
    public float treeFallDuration = 6f;
    private Rigidbody treeRigidbody = null;
    [SerializeField] public GameObject treeToDisable = null;
    [SerializeField] public GameObject planksToSpawn = null;
    private bool done = false;
    public Vector3 forcePower = new Vector3(1f,0f,0f);

    private GameObject player = null;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        treeRigidbody = GetComponent<Rigidbody>();
    }

    public void FellTree()
    {
        StartCoroutine("TreeFallSequence");
    }

    IEnumerator TreeFallSequence()
    {
        FMODUnity.RuntimeManager.PlayOneShot(treeFall);
        //treeRigidbody.isKinematic = false;

        treeRigidbody.AddForceAtPosition(forcePower, player.gameObject.transform.position);
        // treeRigidbody.AddForceAtPosition(Vector3.zero, Vector3.zero, ForceMode.Impulse);

        yield return new WaitForSeconds(treeFallDuration);
        treeToDisable.SetActive(false);
        planksToSpawn.SetActive(true);

        yield return null;
    }
}
