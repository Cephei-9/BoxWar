using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCrush : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float _magnitude = 1;
    [SerializeField] private float _decraceMove = 0.2f;
    [SerializeField] private float _speedMove = 5;
    [SerializeField] private float _timeMove = 0.5f;
    [Header("Rotation")]
    [SerializeField] private float _maxAngle = 1;
    [SerializeField] private float _decraceRotation = 0.2f;
    [SerializeField] private float _speedRotate = 5;
    [SerializeField] private float _timeRotate = 0.5f;
    [Space]
    [SerializeField] private float _speedNormalize = 0.5f;
    [SerializeField] private float _pogreshnost = 0.5f;
    [Space]
    [SerializeField] private Transform _cameraTransform;

    public bool IsCrush { get => _isPositionCrush && _isRotationCrush; }
    private bool _isPositionCrush = false;
    private bool _isRotationCrush = false;

    private Transform _beforCrushTransform;

    public void AddCrush(float multiply)
    {
        if (IsCrush) StopAllCoroutines();

        _beforCrushTransform = _cameraTransform;

        StartCoroutine(CrushCameraPosition(_magnitude * multiply, _decraceMove * multiply, _speedMove * multiply, _timeMove * multiply));
        StartCoroutine(CrushCameraRotate(_maxAngle * multiply, _decraceRotation * multiply, _speedRotate * multiply, _timeRotate * multiply));

        _isPositionCrush = true;
        _isRotationCrush = true;

        StartCoroutine(NormolizeCamera());
    }

    private IEnumerator CrushCameraPosition(float magnitude, float decraceMagnitude, float speed, float time)
    {
        Vector3 startPosition = _cameraTransform.localPosition;
        float timer = 0;

        while (timer < time)
        {
            Vector3 newPosition = startPosition + (Vector3)Random.insideUnitCircle.normalized * Random.Range(0.7f, 1) * magnitude;
            while (timer < time)
            {
                print("Corutine is work");
                timer += Time.deltaTime;
                _cameraTransform.localPosition = Vector3.MoveTowards(_cameraTransform.localPosition, newPosition, speed * Time.deltaTime);
                if (Vector3.Distance(_cameraTransform.localPosition, newPosition) < speed * Time.deltaTime)
                {
                    break;
                }
                yield return null;
            }
            magnitude *= decraceMagnitude;
            speed *= decraceMagnitude;
        }
        _isPositionCrush = false;
    }

    private IEnumerator CrushCameraRotate(float maxAngle, float decraceMagnitude, float speed, float time)
    {
        Quaternion startRotation = _cameraTransform.localRotation;
        float timer = 0;

        int sign = Random.Range(-1, 2);
        sign = Mathf.RoundToInt(Mathf.Sign(sign));
        while (timer < time)
        {
            Quaternion newRotation = startRotation * Quaternion.AngleAxis(maxAngle * Random.Range(0.7f, 1) * sign, Vector3.forward);
            while (timer < time)
            {
                print("Corutine is work");
                timer += Time.deltaTime;
                _cameraTransform.localRotation = Quaternion.RotateTowards(_cameraTransform.localRotation, newRotation, speed * Time.deltaTime);
                if (Quaternion.Angle(_cameraTransform.localRotation, newRotation) < speed * Time.deltaTime)
                {
                    break;
                }
                yield return null;
            }
            maxAngle *= decraceMagnitude;
            speed *= decraceMagnitude;
            sign *= -1;
        }
        _isRotationCrush = false;
    }

    private IEnumerator NormolizeCamera()
    {
        yield return new WaitUntil(() => IsCrush == false);

        while (Vector3.Distance(_cameraTransform.localPosition, _beforCrushTransform.localPosition) > _pogreshnost)
        {
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _beforCrushTransform.localPosition,
                _speedNormalize * Time.deltaTime);
            _cameraTransform.localRotation = Quaternion.Lerp(_cameraTransform.localRotation, _beforCrushTransform.localRotation, 
                _speedNormalize * Time.deltaTime);
            yield return null;
        }

        _cameraTransform.localPosition = _beforCrushTransform.localPosition;
        _cameraTransform.localRotation = _beforCrushTransform.localRotation;
    }
}
