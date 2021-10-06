using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowBoxManager : MonoBehaviour
{
    [SerializeField] private float _speedBoxToTarget = 5;
    [SerializeField] private float _speedBoxToBox = 5;
    [SerializeField] private float _antiMagnitDistanse = 1;
    [Space]
    [SerializeField] private float _minLocalScale = 0.8f;
    [SerializeField] private float _maxLocalScale = 1f;
    [Space]
    [SerializeField] private List<Rigidbody> _boxes;
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _tankRb;

    private void Start()
    {
        foreach (var box in _boxes)
        {
            box.transform.localScale = Vector3.one * Random.Range(_minLocalScale, _maxLocalScale);
        }
    }

    private void FixedUpdate()
    {
        FolowTarget();
    }

    private void FolowTarget()
    {
        foreach (var box in _boxes)
        {
            Vector3 vectorToTarget = _target.position - box.position;
            float distanse = vectorToTarget.magnitude;
            Vector3 speed = vectorToTarget.normalized * _speedBoxToTarget * distanse * distanse * distanse;
            box.AddForce(speed, ForceMode.VelocityChange);
            AntiMagnit(box);
        }
    }

    private void AntiMagnit(Rigidbody box)
    {
        foreach (var box2 in _boxes)
        {
            Vector3 vectorToBox = box2.position - box.position;
            if (vectorToBox.magnitude < _antiMagnitDistanse)
            {
                float multiply = Mathf.InverseLerp(_antiMagnitDistanse, 0, vectorToBox.magnitude);
                box2.AddForce(vectorToBox.normalized * multiply * _speedBoxToBox, ForceMode.VelocityChange);
            }
        }
    }
}
