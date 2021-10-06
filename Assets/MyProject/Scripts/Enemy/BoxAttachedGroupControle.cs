using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAttachedGroupControle : PlayerAttachedControle
{
    [SerializeField] private int _countEnemyToPlayerAttack = 4;
    [Space]
    [SerializeField] private Transform _boxTransform; // При активации бокс или танк будут скидывать сюда свой трансформ

    private List<Transform> _attackEnemies = new List<Transform>();
    private List<Transform> _waitingEnemies = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyAI enemyAI))
        {
            if (enemyAI.InFight == false) AskCanAttack(enemyAI);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyAI enemyAI))
        {
            _waitingEnemies.Remove(enemyAI.transform);
        }
    }
    
    public void KickOutAllEnemy()
    {
        foreach (var item in _attackEnemies)
        {
            
        }
    }

    public override void ExitFight(EnemyAI enemyAI)
    {
        _attackEnemies.Remove(enemyAI.transform);
        if (_attackEnemies.Count < _countEnemyToPlayerAttack) InviteEnemyToFight();
    }

    public bool AskCanAttack(EnemyAI enemyAI)
    {
        if (_attackEnemies.Count < _countEnemyToPlayerAttack)
        {
            _attackEnemies.Add(enemyAI.transform);
            enemyAI.GetTransformInAttachedObj(this, _boxTransform);
            return true;
        }
        _waitingEnemies.Add(enemyAI.transform);
        return false;
    }

    public void InviteEnemyOnCollision(Collision collision)
    {
        if (collision.rigidbody.GetComponent<AllComponent>() == null) return;
        collision.rigidbody.GetComponent<AllComponent>().SerchComponentByType(new EnemyAI().GetType(), out Component component);
        EnemyAI enemyAI = component as EnemyAI;

        if (_attackEnemies.Contains(enemyAI.transform)) return;
        if (enemyAI.IsAlive == false) return;

        enemyAI.ExitAttachedObj();
        enemyAI.GetTransformInAttachedObj(this, _boxTransform);
        _attackEnemies.Add(enemyAI.transform);

        if (_waitingEnemies.Contains(enemyAI.transform)) _waitingEnemies.Remove(enemyAI.transform);
        if (_attackEnemies.Count > _countEnemyToPlayerAttack) KickOutEnemy();
    }

    private void InviteEnemyToFight()
    {
        if (_waitingEnemies.Count == 0) return;
        List<Transform> noNullTransform = SerchTransform.CleinIEnumerableAtNull(_waitingEnemies);
        //
        noNullTransform = new List<Transform>();
        foreach (var item in _waitingEnemies)
        {
            if (item != null && item.GetComponent<EnemyAI>().IsAlive) noNullTransform.Add(item);
        }
        //
        EnemyAI enemyAI = null;

        while (noNullTransform.Count > 0)
        {
            SerchTransform.SerchNearest(_boxTransform, noNullTransform.ToArray(), out int returnTransformIndex);
            EnemyAI enemyAILocal = noNullTransform[returnTransformIndex].GetComponent<EnemyAI>();
            noNullTransform.RemoveAt(returnTransformIndex);

            if (enemyAILocal.InFight) continue;

            enemyAI = enemyAILocal;
            _waitingEnemies.RemoveAt(returnTransformIndex);
            break;
        }

        if (enemyAI == null) return;
        enemyAI.GetTransformInAttachedObj(this, _boxTransform);
        _attackEnemies.Add(enemyAI.transform);
    }

    private void KickOutEnemy()
    {
        SerchTransform.SerchFarthest(_boxTransform, _attackEnemies.ToArray(), out int returnTransformIndex);
        EnemyAI enemyAI = _attackEnemies[returnTransformIndex].GetComponent<EnemyAI>();

        if (Vector3.Distance(_boxTransform.position, enemyAI.transform.position) < transform.localScale.x / 2)
        {
            _waitingEnemies.Add(enemyAI.transform);
        }
        enemyAI.ExitAttachedObj();
    }
}
