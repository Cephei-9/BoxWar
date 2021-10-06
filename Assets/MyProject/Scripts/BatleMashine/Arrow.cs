using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    [Header("Arrow")]
    [SerializeField] private float _rotateSpeed = 0.1f;
    [SerializeField] private float _aimRotateSpeed = 20;

    private float _aimSpeed;
    private Transform _aimTransform;

    protected override void Start()
    {
        base.Start();        
    }

    private void Update()
    {
        if (_aimTransform)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_aimTransform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _aimRotateSpeed * Time.deltaTime);
            _selfRb.velocity = transform.forward * _aimSpeed;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(_selfRb.velocity);
        }
    }

    public void SetParametrsToAim(Transform aimTransform)
    {
        _aimTransform = aimTransform;
        _aimSpeed = _selfRb.velocity.magnitude;
    }

    protected override void OnCollision(Collision collision)
    {
        _selfRb.isKinematic = true;
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
