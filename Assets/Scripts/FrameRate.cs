using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour {

    [SerializeField] private int _targetFrameRate = 60;

    void Start() {
        Application.targetFrameRate = _targetFrameRate;
    }

}
