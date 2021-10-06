using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulemetBullet : Bullet
{
    [SerializeField] private float _dieDistance = 50;

    protected override void Start()
    {
        float timeDeath = _dieDistance / _selfRb.velocity.magnitude;
        Destroy(gameObject, timeDeath);
    }
}
