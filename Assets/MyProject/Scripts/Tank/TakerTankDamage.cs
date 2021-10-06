using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakerTankDamage : MonoBehaviour
{
    [SerializeField] private float _minDamageOnCollision = 2;
    [SerializeField] private float _maxDamageOnCollision = 22;

    [SerializeField] private float _decraceDamage = 1;
    [Space]
    [SerializeField] private Health _selfHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == null) return;

        if (collision.rigidbody.TryGetComponent(out Tank tank))
        {
            TankСharacteristics сharacteristics = tank.selfCharacteristics;
            float damage = сharacteristics.VelositiOnCollion.magnitude * сharacteristics.Damage * _decraceDamage;
            float clampedDaamage = Mathf.Clamp(damage, _minDamageOnCollision, _maxDamageOnCollision);
            _selfHealth.TakeDamage(clampedDaamage, tank._selfHealth);
        }
    }
}

