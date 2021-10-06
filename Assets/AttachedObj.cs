using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedObj : PlayerAttachedControle
{
    [SerializeField] private Transform[] _attachedPoint;

    private List<Transform> _freeAttachedPoint = new List<Transform>();
    private Dictionary<EnemyAI, Transform> _attackEnemies = new Dictionary<EnemyAI, Transform>();
    private List<Transform> _waitingEnemies = new List<Transform>();

    public bool CanAttack { get => _freeAttachedPoint.Count > 0; }

    private void Start()
    {
        _freeAttachedPoint.AddRange(_attachedPoint);
    }

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

    private bool AskCanAttack(EnemyAI enemyAI)
    {
        if (CanAttack)
        {
            GiveEnemyTransform(enemyAI);
            return true;
        }
        _waitingEnemies.Add(enemyAI.transform);
        return false;
    }

    private void GiveEnemyTransform(EnemyAI enemyAI)
    {
        Transform attackPoint = SerchTransform.SerchNearest(enemyAI.transform, _freeAttachedPoint.ToArray(), out int index);
        _attackEnemies.Add(enemyAI, attackPoint);
        _freeAttachedPoint.Remove(attackPoint);

        enemyAI.GetTransformInAttachedObj(this, attackPoint);
    }

    public override void ExitFight(EnemyAI selfAI)
    {
        try
        {
            _freeAttachedPoint.Add(_attackEnemies[selfAI]);
        }
        catch (System.Exception)
        {
            Debug.LogError("NoKeyValue");
            Debug.Break();
            throw;
        }
        _attackEnemies.Remove(selfAI);
        InviteEnemyToFight();
    }

    private void InviteEnemyToFight()
    {
        List<Transform> noNullTransform = SerchTransform.CleinIEnumerableAtNull(_waitingEnemies);
        //
        noNullTransform = new List<Transform>();
        foreach (var item in _waitingEnemies)
        {
            if (item != null) noNullTransform.Add(item);
        }
        //
        EnemyAI enemyAI = null;

        while (noNullTransform.Count > 0)
        {
            SerchTransform.SerchNearest(transform, noNullTransform.ToArray(), out int returnTransformIndex);
            EnemyAI enemyAILocal = noNullTransform[returnTransformIndex].GetComponent<EnemyAI>();

            noNullTransform.RemoveAt(returnTransformIndex);
            if (enemyAILocal.InFight) continue;

            enemyAI = enemyAILocal;
            _waitingEnemies.RemoveAt(returnTransformIndex);
            break;
        }

        if (enemyAI) GiveEnemyTransform(enemyAI);
    }
}
