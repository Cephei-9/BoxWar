using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [Header("Enemy")]
    public float addPlayerHealth = 0.25f;

    public override void TakeDamage(float damage, Health otherHealth)
    {
        base.TakeDamage(damage, otherHealth);
    }

    public override void Die(Health otherHealth)
    {
        if (otherHealth is PlayerHealth)
        {
            (otherHealth as PlayerHealth).AddHealth(startHealth * addPlayerHealth);
        }
        base.Die(otherHealth);
    }

    private void OnDestroy()
    {
        if(IsAlive) Die(new Health());
    }
}
