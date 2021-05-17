using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerHere : MonoBehaviour
{
    private PlayerMovement player = null;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        player.gameObject.transform.position = gameObject.transform.position;
        player.gameObject.transform.rotation = gameObject.transform.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(Vector3.zero, new Vector3(0.5f, 1, 0.5f));
        Gizmos.DrawLine(Vector3.zero, Vector3.zero + Vector3.forward);
    }
}
