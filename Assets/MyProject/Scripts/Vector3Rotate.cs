using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Rotate : MonoBehaviour
{
    static Transform workTransform;

    private void Start()
    {
        workTransform = new GameObject().transform;
    }

    static public Vector3 RotateVectorOnAngleInAxis(Vector3 vectorToRotate, float angle, Vector3 axis)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        workTransform.rotation = Quaternion.LookRotation(vectorToRotate);
        workTransform.rotation *= rotation;
        return workTransform.forward;
    }
}
