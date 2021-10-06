using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour {

    [SerializeField] private bool _unparent;
    [SerializeField] private Transform _target;

    void Start() {
        if (_unparent) {
            transform.parent = null;
        }
    }

    private void LateUpdate() {
        transform.position = _target.position;
    }

}
