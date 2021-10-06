using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMoveToTarget : MonoBehaviour
{
    [SerializeField] float _speed = 2;
    [Space]
    [SerializeField] Rigidbody _selfRb;
    [SerializeField] Transform _target;

    private void FixedUpdate()
    {
        Vector3 vectorToTarget = _target.position - transform.position;
        _selfRb.AddForce(vectorToTarget.normalized * _speed, ForceMode.VelocityChange);
    }
}
