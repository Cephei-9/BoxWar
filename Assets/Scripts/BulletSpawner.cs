using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    [SerializeField] private EnemyBullet _enemyBulletPrefab;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _shotPeriod = 5f;

    private float _timer;

    void Start() {
        _timer = Random.Range(0, _shotPeriod);
    }

    void Update() {
        _timer += Time.deltaTime;
        if (_timer > _shotPeriod) {
            _timer = 0f;
            Shot();
        }
    }

    void Shot() {
        EnemyBullet enemyBullet = Instantiate(_enemyBulletPrefab, transform.position, Quaternion.identity);
        enemyBullet.SetVelocity(transform.forward * _bulletSpeed);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.forward * 12f);
    }

}
