using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorBounceForTank : MonoBehaviour
{
    public Vector3 takeDamageVelosity;

    public Vector3 AddBounce(Rigidbody selfRb, Rigidbody enemyRb)
    {
        Vector3 normal = enemyRb.position - selfRb.position;
        Vector3 projectNormal = Vector3.ProjectOnPlane(normal, Vector3.up);
        Vector3 perpendicularNormal = Vector3Rotate.RotateVectorOnAngleInAxis(projectNormal, 90, Vector3.up);

        Vector3 selfBounce = SelfBounce(perpendicularNormal, normal, selfRb, enemyRb);
        Vector3 enemyBounce = EnemyBounce(perpendicularNormal, normal, selfRb, enemyRb);

        return (selfBounce + enemyBounce);
    }

    private Vector3 SelfBounce(Vector3 perpendicularNormal, Vector3 normal, Rigidbody selfRb, Rigidbody enemyRb)
    {
        Vector3 reflectDirection = Vector3.Reflect(selfRb.velocity, normal.normalized);

        float angleKoificent = Vector3.Dot(selfRb.velocity.normalized, normal.normalized);
        if (angleKoificent <= 0) angleKoificent = 1; // если он меньше 0 то тогда после инверс лерпа он станет 0. Так он сам устраняется
        angleKoificent = Mathf.InverseLerp(1, 0, angleKoificent);
        angleKoificent = (angleKoificent * 0.5f) + 0.5f; // чтобы при ударе в лоб он был 0.5 а при касательном ближе к 1      

        float massKoificent = Mathf.InverseLerp(0, selfRb.mass, enemyRb.mass);

        Vector3 bounceVector = reflectDirection * angleKoificent * massKoificent;
        return bounceVector;
    }

    private Vector3 EnemyBounce(Vector3 perpendicularNormal, Vector3 normal, Rigidbody selfRb, Rigidbody enemyRb)
    {
        Vector3 reflectDirection = Vector3.Reflect(enemyRb.velocity, perpendicularNormal);

        float angleKoificent = Vector3.Dot(enemyRb.velocity.normalized, -normal.normalized);
        float massKoificent = enemyRb.mass / selfRb.mass;

        takeDamageVelosity = Vector3.ProjectOnPlane(enemyRb.velocity, Vector3.up) * angleKoificent;
        Vector3 bounceVector = reflectDirection * angleKoificent * massKoificent;
        return bounceVector;
    }
}
