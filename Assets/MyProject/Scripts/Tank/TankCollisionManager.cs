using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCollisionManager : MonoBehaviour
{
    public void DoWork(Tank tank1, Tank tank2)
    {
        if (tank1.isCollision)
        {
            tank1.isCollision = false;
            tank2.isCollision = false;
            return;
        }

        CalculatorBounceForTank takeBounce = new CalculatorBounceForTank();
        Vector3 tank1Bounce = takeBounce.AddBounce(tank1._boxRb, tank2._boxRb);
        //Prorortion
        TankDamageCalculator damageCalculator = tank1.selfCharacteristics.DamageCalculator;
        damageCalculator.TakeDamage(takeBounce.takeDamageVelosity, tank2.selfCharacteristics.Damage, tank2._selfHealth);

        Vector3 tank2Bounce = takeBounce.AddBounce(tank2._boxRb, tank1._boxRb);
        damageCalculator = tank2.selfCharacteristics.DamageCalculator;
        damageCalculator.TakeDamage(takeBounce.takeDamageVelosity, tank1.selfCharacteristics.Damage, tank1._selfHealth);

        tank1.AddBounce(tank1Bounce);
        tank2.AddBounce(tank2Bounce);
    }
}
