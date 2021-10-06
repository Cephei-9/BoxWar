using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankDamageCalculator : MonoBehaviour
{
    [SerializeField] protected float _startMultiplyDamageInSelfHelthCollision = 0.2f;
    [SerializeField] protected float MinDamageOnCollision = 8;
    [SerializeField] protected float MaxDamageOnCollision = 20;
    [Space]
    [SerializeField] protected Health _selfHealth;

    protected float _multiplyDamageInSelfHelthCollision = 0.2f;

    protected virtual void Start()
    {
        _multiplyDamageInSelfHelthCollision = _startMultiplyDamageInSelfHelthCollision;
    }

    public void TakeDamage(Vector3 damageVelosity, float enemyDamage, Health otherHealth)
    {
        float damage = damageVelosity.magnitude * enemyDamage;
        print("Name: " + transform.parent.name + "Dam: " + damage);
        damage = Mathf.Clamp(damage, MinDamageOnCollision, MaxDamageOnCollision);

        if (otherHealth.GetType() == _selfHealth.GetType())
        {
            damage *= _multiplyDamageInSelfHelthCollision;
        }
        _selfHealth.TakeDamage(damage, otherHealth);
    }
}
