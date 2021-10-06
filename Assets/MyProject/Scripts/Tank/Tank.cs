using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tank : MonoBehaviour
{
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _bounceMultiply = 3;
    [SerializeField] private float _decraceVelosity = 0.5f;
    [Space]
    [SerializeField] private float AddedBounce = 8;
    [SerializeField] private float MinBounce = 8;
    [SerializeField] private float MaxBounce = 20;
    [Space]
    [SerializeField] private float _multiplyDamageInSelfHelthCollision = 0.2f;
    [SerializeField] private float _startMultiplyDamageInSelfHelthCollision = 0.2f;
    [SerializeField] private float MinDamageOnCollision = 8;
    [SerializeField] private float MaxDamageOnCollision = 20;
    [Space]
    public Rigidbody _boxRb;
    public Rigidbody _selfRb;
    [Space]
    [SerializeField] public Health _selfHealth;

    [HideInInspector] public bool isCollision = false;
    [HideInInspector] public Tank—haracteristics selfCharacteristics;

    public UnityEvent<Collision> OnTankCollision;
    public UnityEvent<float> OnTakeBounce;

    private void Start()
    {
        selfCharacteristics = new Tank—haracteristics(_damage, _boxRb, GetComponent<TankDamageCalculator>());
    }

    private void FixedUpdate()
    {
        _selfRb.MovePosition(_boxRb.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Tank>())
        {
            TankCollisionManager tankCollision = new TankCollisionManager();
            tankCollision.DoWork(this, collision.collider.GetComponent<Tank>());

            OnTankCollision.Invoke(collision);
        }
    }

    public void AddBounce(Vector3 bounce)
    {
        _boxRb.velocity *= _decraceVelosity;

        float clampedBounce = Mathf.Clamp((bounce.magnitude * _bounceMultiply) + AddedBounce, MinBounce, MaxBounce);
        bounce = bounce.normalized * clampedBounce;

        OnTakeBounce.Invoke(Mathf.InverseLerp(0, MaxBounce, clampedBounce));
        _boxRb.AddForce(bounce, ForceMode.VelocityChange);
        isCollision = true;
    }

    public void AddImpuls(Vector3 impuls)
    {
        _boxRb.AddForce(impuls , ForceMode.Impulse);
    }

    public void TakeDamage(Vector3 damageVelosity, float enemyDamage, Health otherHealth)
    {
        float damage = damageVelosity.magnitude * enemyDamage;
        print("Name: " + transform.parent.name + "Dam: " + damage);
        damage = Mathf.Clamp(damage, MinDamageOnCollision, MaxDamageOnCollision);

        if (otherHealth.GetType() == _selfHealth.GetType())
        {
            damage *= _multiplyDamageInSelfHelthCollision;
        }
        _selfHealth.TakeDamage(damage, otherHealth);
    }
}

[System.Serializable]
public class Tank—haracteristics
{
    public readonly float Damage;
    public float Mass { get => _tankRb.mass; } // ¬ ÔÓÒÎÂ‰ÒÚ‚ËË ÏÓÊÌÓ ÔÓÏÂÌˇÚ¸ Ì‡ ÓÚÚÂÎ¸ÌÓÂ ÔÓÎÂ
    public Vector3 VelositiOnCollion { get => _tankRb.velocity; }
    public readonly TankDamageCalculator DamageCalculator;

    [SerializeField] private Rigidbody _tankRb;

    public Tank—haracteristics(float damage, Rigidbody tankRb, TankDamageCalculator damageCalculator)
    {
        Damage = damage;
        _tankRb = tankRb;
        DamageCalculator = damageCalculator;
    }
}
