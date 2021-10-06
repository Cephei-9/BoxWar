using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TankDamageCalculatorEnemy : TankDamageCalculator
{
    private List<PlayerAttachedControle> attachedControles = new List<PlayerAttachedControle>();

    protected override void Start()
    {
        _multiplyDamageInSelfHelthCollision = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerAttachedControle attachedControle))
        {
            //print("Activate damage on self fraction 111111");
            attachedControles.Add(attachedControle);
            ActiveDamageFromSelfFraction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerAttachedControle attachedControle))
        {
            attachedControles.Remove(attachedControle);
            if (attachedControles.Count == 0)
            {
                DeactiveDamageFromSelfFraction();
                //print("Deactivate damage on self fraction 000000");
            }
        }
    }

    public void ActiveDamageFromSelfFraction()
    {
        _multiplyDamageInSelfHelthCollision = _startMultiplyDamageInSelfHelthCollision;

    }

    public void DeactiveDamageFromSelfFraction()
    {
        _multiplyDamageInSelfHelthCollision = 0;

    }
}
