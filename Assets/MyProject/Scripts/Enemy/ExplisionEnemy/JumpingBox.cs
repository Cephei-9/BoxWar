using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBox : MonoBehaviour
{
    [Header("JumpBox")]
    [SerializeField] private Transform _boxTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [Space]
    [SerializeField] private float _forceMultiplier = 5f;
    [SerializeField] private float _jumpHight= 5f;
    [SerializeField] private float _velosityToStart = 0.3f;
    [Space]
    public float Charge = 1f;
    public float _speedCharge = 1f;
    [Space]
    [SerializeField] private EnemyAI selfAI;

    private void Start()
    {
        _rigidbody.centerOfMass = new Vector3(0f, 0.1f, 0f);
    }

    private void Update()
    {
        Jump();
        Charge += Time.deltaTime * _speedCharge;
    }

    private void Jump()
    {
        if (Charge > 0.99f)
        {
            if (_rigidbody.velocity.magnitude > _velosityToStart) return;

            Vector3 _vectorToTarget = selfAI.TargetPosition - transform.position;
            Vector3 _vectorToTargetOnPlane = Vector3.ProjectOnPlane(_vectorToTarget, Vector3.up);
            Vector3 velocity = _vectorToTargetOnPlane.normalized * _forceMultiplier + Vector3.up * _jumpHight;
            _rigidbody.AddForce(velocity, ForceMode.VelocityChange);

            Charge = 0;
        }
    }
}
