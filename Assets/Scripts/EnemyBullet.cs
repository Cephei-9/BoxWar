using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : EnemyBase {

    public Collider Collider;
    [SerializeField] private float _damageValue;

    protected override void Start() {
        base.Start();
        Invoke("Die", 8f);
    }

    public void SetVelocity(Vector3 velocity) {
        _rigidbody.velocity = velocity;
    }

    public override void OnCollisionWithPlayer(PlayerBox playerBox, Collision collision) {
        base.OnCollisionWithPlayer(playerBox, collision);

        playerBox.TakeDamage(_damageValue);
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody) {
            if (collision.rigidbody.GetComponent<PlayerBox>()) {
                return;
            }
        }
        Die(Vector3.zero);
    }

}
