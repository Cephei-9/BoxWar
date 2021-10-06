using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulmetAim : AimBatleMashine
{
    public float timeBetwinBullet = 0.18f;
    public float timeToPeregrev = 5f;
    public float timePeregrev = 3f;
    [Space]
    public float _speedRotateMashine = 0.5f;

    private float timerForBullet;
    private float timerForPeregrev;

    private bool isPeregrev = false;

    private void Update()
    {
        Aim();
    }

    public void Aim()
    {
        timerForBullet += Time.deltaTime;
        timerForPeregrev += Time.deltaTime;

        if (_actualEenemy)
        {
            RotateBatleMashine(_actualEenemy.position);

            if (timerForBullet > timeBetwinBullet && isPeregrev == false)
            {
                if (timerForPeregrev > timeToPeregrev)
                {
                    isPeregrev = true;
                    timerForPeregrev = 0;
                }
                batleMashine.AimShot(1);
                timerForBullet = 0;
            }
            else if (isPeregrev && timerForPeregrev > timePeregrev)
            {
                isPeregrev = false;
                timerForPeregrev = 0;
            }
        }
        else
        {
            RotateBatleMashine(batleMashine._rotateTransform.position + transform.forward);
        }
    }

    private void OnDrawGizmos()
    {
        if (_actualEenemy)
            Gizmos.DrawSphere(_actualEenemy.position + Vector3.up * 3, 2);
    }

    public override void RotateBatleMashine(Vector3 enemyPosition)
    {
        Quaternion toEnemy = Quaternion.LookRotation(enemyPosition - batleMashine._rotateTransform.position);
        batleMashine._targetRotation = Quaternion.Lerp(batleMashine._targetRotation, toEnemy, _speedRotateMashine * Time.deltaTime);
    }
}
