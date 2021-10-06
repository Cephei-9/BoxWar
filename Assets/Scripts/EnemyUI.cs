using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour {

    [SerializeField] private Transform _target;
    [SerializeField] private Enemy _enemy;

    private void Start() {
        transform.parent = null;
        _enemy.EventOnDie.AddListener(OnDie);
    }

    void LateUpdate() {
        transform.position = _target.position;
    }

    void OnDie() {
        Destroy(gameObject);
    }

}
