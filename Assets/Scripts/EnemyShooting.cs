using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy {

    [Header("EnemyShooting")]
    [SerializeField] private EnemyBullet _enemyBulletPrefab;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _shotPeriod = 5f;
    private float _timer;

    private Vector3 _startScale;
    [SerializeField] private AnimationCurve _scaleCurve;

    protected override void Start() {
        base.Start();
        _startScale = transform.localScale;
        _timer = Random.Range(0, _shotPeriod);
    }

    protected override void Update() {
        base.Update();
        if (!_isActive) return;

        _timer += Time.deltaTime;
        if (_timer > _shotPeriod) {
            _timer = 0f;
            StartCoroutine(ShotAnimation());
        }
    }

    IEnumerator ShotAnimation() {
        for (float t = 0; t < 1f; t += Time.deltaTime * 2f) {
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = _startScale * scale;
            yield return null;
        }
        transform.localScale = _startScale;
        CreateBullet();
    }

    void CreateBullet() {

        Vector3 toPlayer = _playerBox.transform.position - transform.position;
        Vector3 velocity = toPlayer.normalized * _bulletSpeed;

        EnemyBullet enemyBullet = Instantiate(_enemyBulletPrefab, transform.position, Quaternion.identity);
        enemyBullet.SetVelocity(velocity);
        Physics.IgnoreCollision(GetComponent<Collider>(), enemyBullet.Collider);

    }

}
