using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaAim : AimBatleMashine
{
    [Header("Balista")]
    [SerializeField] private float _damageInAim = 5;

    [SerializeField] private float _timeToAimArrow = 8;
    [SerializeField] private float _timeToSimpleShoot = 8;
    [Space]
    [SerializeField] private float _minDistanceFlyArrow;
    [SerializeField] private float _maxDistanceFlyArrow;
    [Space]
    [SerializeField] private float _pogreshnost = 0.1f;

    private float timerToAim;
    private float _timerToSimple;

    private void Update()
    {
        Aim();
    }

    public void Aim()
    {
        if (_actualEenemy)
        {
            RotateBatleMashine(_actualEenemy.position);

            timerToAim += Time.deltaTime;
            _timerToSimple += Time.deltaTime;

            if (timerToAim > _timeToAimArrow && _timerToSimple > _timeToSimpleShoot)
            {
                float multiply = CalculateMultiply();
                Arrow arrow = (batleMashine as Balista).AimShot(multiply);

                arrow.SetParametrsToAim(_actualEenemy);
                arrow._damage = _damageInAim;

                timerToAim = _timerToSimple = 0;
            }
            else if (_timerToSimple > _timeToSimpleShoot)
            {
                batleMashine.Shot(CalculateMultiply() + Random.Range(-_pogreshnost, _pogreshnost));
                _timerToSimple = 0;
            }
        }
    }

    private float CalculateMultiply()
    {
        Balista balista = (batleMashine as Balista);

        Vector3 vectorToEnemy = _actualEenemy.position - batleMashine.transform.position;
        float inversToEnemy = Mathf.InverseLerp(_minDistanceFlyArrow, _maxDistanceFlyArrow, vectorToEnemy.magnitude);
        float multiply = balista.MinMultiply + inversToEnemy * (1 - balista.MinMultiply);
        return multiply;
    }

    public override void RotateBatleMashine(Vector3 enemyPosition)
    {
        Vector3 rotation = enemyPosition - batleMashine.transform.position;
        rotation.y = 0;
        batleMashine._targetRotation = Quaternion.LookRotation(rotation);
    }

    private void OnDrawGizmos()
    {
        if(_actualEenemy)
            Gizmos.DrawSphere(_actualEenemy.position + Vector3.up * 3, 2);
    }
}
