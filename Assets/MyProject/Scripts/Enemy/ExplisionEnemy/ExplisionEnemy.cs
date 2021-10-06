using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplisionEnemy : MonoBehaviour
{
    [SerializeField] private float _damage = 5;
    [SerializeField] private float _impuls = 100;
    [SerializeField] private float _radius = 2;
    [SerializeField] private float _upwardsModifier = 1;
    [Space]
    [SerializeField] private float _smoothingDistance = 1;
    [SerializeField] private float _minDistance = 1;
    [Space]
    [SerializeField] private float _timeToBeginExplision = 0.7f;
    [SerializeField] private float _timeToExplision = 2;
    [SerializeField] private float _timeToExplosionAfterDeath = 1;
    [SerializeField] private float _distanceToExplision = 4;
    [SerializeField] private float _damageToResetExplision = 4;
    [Space]
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private JumpingBox _boxMove;
    [SerializeField] private Transform _ExplisionSpher;

    private float _timer;
    private bool _beginExplision;

    private void Start()
    {
        transform.localScale = Vector3.one * (_distanceToExplision + 1.5f);
    }

    private void Update()
    {
        if (_beginExplision) return;
        
        bool visonPlayerHealth = false;
        foreach (var item in Physics.OverlapSphere(transform.position, _distanceToExplision))
        {
            if (item.GetComponent<PlayerHealth>())
            {
                visonPlayerHealth = true;
                break;
            }
        }
        if (visonPlayerHealth == false)
        {
            _timer = 0;
            return;
        }
        _boxMove.Charge = 0;

        _timer += Time.deltaTime;
        if (_timer >= _timeToBeginExplision) StartCoroutine(ExplosionPreparation(0));
    }

    public void ResetExplosionOnTakeDamage(float damage, Health otherHealth) // No usege but idea is intresting
    {
        return;
        if (damage < _damageToResetExplision) return;
        _beginExplision = false;
        _ExplisionSpher.localScale = Vector3.zero;
        _timer = 0;
    }

    public void ExplosionAfterDeath()
    {
        if (_beginExplision || gameObject.activeSelf == false) return;

        StartCoroutine(ExplosionPreparation(_timeToExplosionAfterDeath));
    }

    private IEnumerator ExplosionPreparation(float timeToExplosion)
    {
        _beginExplision = true;
        _boxMove.enabled = false;
        float timer = 0;

        if (timeToExplosion != 0) yield return new WaitForSeconds(timeToExplosion);

        while (true)
        {
            timer += Time.deltaTime;
            _ExplisionSpher.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * (_radius + 1.5f), timer / _timeToExplision);

            if (timer > _timeToExplision)
            {
                Explosion();
                Destroy(transform.parent.gameObject);
                break;
            }
            yield return null;
        }
    }

    private void Explosion()
    {
        if (gameObject.activeSelf == false) return;

        List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(transform.position, _radius));
        Collider[] selfColliders = GetComponentsInChildren<Collider>();
        colliders.Remove(GetComponent<Collider>());
        foreach (var item in selfColliders)
        {
            colliders.Remove(item);
        }

        foreach (var collider in colliders)
        {
            Vector3 vectorToCollider = collider.transform.position - transform.position;
            Vector3 forceVector = vectorToCollider + Vector3.up * _upwardsModifier;

            if (collider.TryGetComponent(out Tank tank))
            {
                float smothDistance = Smothnes(vectorToCollider.magnitude, _radius);
                Vector3 impuls = forceVector.normalized * _impuls / smothDistance;
                Vector3 speed = impuls / tank.selfCharacteristics.Mass;

                tank.AddImpuls(impuls + Vector3.up);
            }
            else if (collider.TryGetComponent(out Health health))
            {
                float smothDistance = Smothnes(vectorToCollider.magnitude, _radius);

                //print("Name: " + health.transform.parent.name);
                //print("Distance: " + vectorToCollider.magnitude);
                //print("SmothDist: " + smothDistance);
                //print("Damage: " + (_damage / smothDistance));
                
                health.TakeDamage(_damage / smothDistance, new EnemyHealth());
            }
        }
    }

    private float Smothnes(float value, float maxValue)
    {
        float inverseValue = Mathf.InverseLerp(0, _radius, value);
        float valueMultiply = value - (inverseValue * inverseValue * maxValue * _smoothingDistance);
        float clamp = Mathf.Clamp(valueMultiply, _minDistance, Mathf.Infinity);
        return clamp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
