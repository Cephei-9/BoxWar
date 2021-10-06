using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour {

    protected PlayerBox _playerBox;
    [HideInInspector] public UnityEvent EventOnDie;
    [SerializeField] protected Rigidbody _rigidbody;

    protected virtual void Start() {
        _playerBox = FindObjectOfType<PlayerBox>();
    }

    public virtual void OnCollisionWithPlayer(PlayerBox playerBox, Collision collision) {
    }


    protected virtual void Die(Vector3 velocity) {
        Destroy(gameObject);
        EventOnDie.Invoke();
    }

}
