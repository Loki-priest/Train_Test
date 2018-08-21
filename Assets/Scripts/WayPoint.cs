using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

    public WayPoint nextWaypoint;
    [SerializeField]
    private Color colorForEditor;

    private void OnDrawGizmos()
    {
        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.color = colorForEditor;

        Gizmos.matrix = cubeTransform;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        Gizmos.matrix = oldMatrix;

        Debug.DrawLine(transform.position, nextWaypoint.transform.position, colorForEditor);

    }
}
