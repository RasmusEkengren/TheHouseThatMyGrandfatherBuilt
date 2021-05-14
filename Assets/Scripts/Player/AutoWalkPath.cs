using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWalkPath : MonoBehaviour
{
	public Transform[] points = null;
	private float gizmoSize = 0.1f;

	void OnDrawGizmosSelected()
	{
		for (int i = 0; i < points.Length; i++)
		{
			if (i == 0)
			{
				Gizmos.DrawSphere(points[i].position, gizmoSize);
			}
			else
			{
				Gizmos.DrawSphere(points[i].position, gizmoSize);
				Gizmos.DrawLine(points[i - 1].position, points[i].position);
			}
		}
	}
}
