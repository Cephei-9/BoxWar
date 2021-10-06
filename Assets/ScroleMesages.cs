using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ScroleMesages : MonoBehaviour
{
    [SerializeField] private float _lowValue = 600;
    [Space]
    [SerializeField] private RectTransform LayautGroup;

    private Vector3 _startPosition;
    private Vector3 _lowerPosition;

    private void Start()
    {
        _startPosition = LayautGroup.transform.position;
        print("Position: " + _startPosition);
        _lowerPosition = _startPosition + Vector3.down * _lowValue;
    }

    public void Scrole(float value)
    {
        LayautGroup.transform.position = Vector3.Lerp(_startPosition, _lowerPosition, value);
    }
}
