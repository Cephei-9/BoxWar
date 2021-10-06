using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _timeDeath = 5;
    [SerializeField] public float _damage = 5;
    [SerializeField] public float _impuls = 20;
    [Space]
    [SerializeField] protected Rigidbody _selfRb;

    protected virtual void Start()
    {
        Destroy(gameObject, _timeDeath);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(_damage, new Health());
            Vector3 vectorToImpuls = transform.forward;
            vectorToImpuls.y = 0;
            other.attachedRigidbody.AddForce(vectorToImpuls * _impuls);
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollision(Collision collision)
    {
        Destroy(gameObject);
    }
}
