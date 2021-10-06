using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParts : MonoBehaviour {

    [SerializeField] private Rigidbody[] _partsRigidbodys;

    public void Push(Vector3 velocity) {
        transform.parent = null;
        gameObject.SetActive(true);
        for (int i = 0; i < _partsRigidbodys.Length; i++) {
            _partsRigidbodys[i].velocity = velocity;
        }
    }

}
