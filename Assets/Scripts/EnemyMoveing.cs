using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveing : Enemy {

    [Header("EnemyMoveing")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private void FixedUpdate() {
        
        if (!_isActive) return;

        Vector3 toPlayer = _playerBox.transform.position - transform.position;
        Vector3 toPlayerXZ = new Vector3(toPlayer.x, 0f, toPlayer.z);
        Quaternion targetRotation = Quaternion.LookRotation(toPlayerXZ, Vector3.up);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.MoveRotation(rotation);
        _rigidbody.velocity = transform.forward * _speed;

    }

}
