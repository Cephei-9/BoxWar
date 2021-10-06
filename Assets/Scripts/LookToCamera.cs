using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LookToCamera : MonoBehaviour {

    private Transform _cameraTransform;

    void Update() {
        if (_cameraTransform == null) {
            _cameraTransform = Camera.main.transform;
        }
        Vector3 vectorToCamera = _cameraTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-1 * vectorToCamera);
    }

}
