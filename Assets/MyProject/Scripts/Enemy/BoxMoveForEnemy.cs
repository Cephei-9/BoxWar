using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxMoveForEnemy : MonoBehaviour
{
    [Header("BoxMove")]
    [SerializeField] private Transform _boxTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _aim;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Gradient _lineGradient;
    [Space]
    [SerializeField] private float _forceMultiplier = 5f;
    [SerializeField] private float _aimDirectionMultiplier = 8f;
    [Space]
    public float Charge = 1f;
    public float _speedCharge = 1f;    
    public float _reduceChargeOnCollision = 1f;    
    [Space]
    [SerializeField] private Slider ChargeSlider;
    [SerializeField] private EnemyAI selfAI;

    private Vector3 _vectorToTarget;
    private Vector3 _vectorToTargetOnPlane;

    private void Start()
    {
        _rigidbody.centerOfMass = new Vector3(0f, 0.1f, 0f);
        Charge = Random.Range(0, 0.7f);
    }

    private void Update()
    {
        _vectorToTarget = selfAI.TargetPosition - transform.position;
        _vectorToTargetOnPlane = Vector3.ProjectOnPlane(_vectorToTarget, Vector3.up);
        SetLenghAimAndLine();
        Jump(); 

        Charge += Time.deltaTime * _speedCharge;
        Charge = Mathf.Clamp(Charge, 0f, 1f);
        //ChargeSlider.value = Charge;
    }

    public void ReduceChargeOnCollision(float value)
    {
        Charge -= value * _reduceChargeOnCollision;
    }

    private void Jump()
    {
        if (Charge > 0.99f)
        {
            Vector3 velocity = _vectorToTargetOnPlane.normalized * _forceMultiplier;
            _rigidbody.velocity = velocity * Charge;
            Charge = 0; 
        }
    }

    private void SetLenghAimAndLine()
    {
        Vector3 pointerPosition = transform.position + (_vectorToTargetOnPlane.normalized * Charge * _aimDirectionMultiplier);

        _aim.transform.position = pointerPosition;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, pointerPosition);

        _lineRenderer.endColor = _lineGradient.Evaluate(Charge);
        _lineRenderer.startColor = _lineGradient.Evaluate(Charge);
    }
}
