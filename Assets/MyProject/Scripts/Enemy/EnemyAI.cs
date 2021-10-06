using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Vector3 _nextRoadPoint;

    public Transform playerTransform { get; private set; }
    public bool canPlayerAttack;

    [SerializeField] private EnemyHealth _selfHealth;
    public PlayerAttachedControle _attachedObj;

    public bool InFight { get => playerTransform != null; } // Поменял _attachedObj на playerTransform
    public bool IsAlive { get => _selfHealth.IsAlive; }

    public Vector3 TargetPosition {
        get {
            if (playerTransform) return playerTransform.position;
            return _nextRoadPoint;
        }
    }

    public void GetTransformInAttachedObj(PlayerAttachedControle attachedControle, Transform transform)
    {
        _attachedObj = attachedControle;
        playerTransform = transform;
    }

    public void ExitAttachedObj()
    {
        if (_attachedObj == null) return;

        _attachedObj.ExitFight(this);
        playerTransform = null;
        _attachedObj = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(TargetPosition, Vector3.one);
    }
}
