using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPoint : MonoBehaviour
{
    private RoadToCastle _roadToCastle;

    private void Start()
    {
        _roadToCastle = GetComponentInParent<RoadToCastle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyAI enemyAI))
        {
            Vector3 nextPointForEnemy = _roadToCastle.GetNextPoint(this, out float nextScale).transform.position;
            enemyAI._nextRoadPoint = GetRandomPositionfromPoint(nextPointForEnemy, nextScale);
        }
    }

    private Vector3 GetRandomPositionfromPoint(Vector3 position, float localSkale)
    {
        Vector2 randomVector = Random.insideUnitCircle;
        Vector3 newPosition = position + (new Vector3(randomVector.x, 0, randomVector.y) * localSkale * 0.5f);
        return newPosition;
    }
}
