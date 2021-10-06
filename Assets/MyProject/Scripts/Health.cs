using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float startHealth = 20;

    [SerializeField] protected GameObject gameObjectToDestroy;
    [SerializeField] protected bool _destroyOnDie = true;
    [Space]
    public UnityEvent<Health> OnDieEvent;
    public UnityEvent<float, Health> OnTakeDamageEvent;
    public bool IsAlive { get => _health > 0; }
    public float CurentHealth { get => _health; }

    protected float _health;
    protected Color StartColor;

    public virtual void Start()
    {
        _health = startHealth;
    }

    public virtual void TakeDamage(float damage, Health otherHealth)
    {
        _health -= damage;
        OnTakeDamageEvent.Invoke(damage, otherHealth);

        if (_health < 0)
        {
            Die(otherHealth);
        }
    }

    public virtual void Die(Health otherHealth)
    {
        if(_destroyOnDie) Destroy(gameObjectToDestroy);
        OnDieEvent.Invoke(this);
    }
}
