using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulimet : BatleMashine
{
    [Header("Pulimet")]
    [SerializeField] private float _maxAngleX = 30;
    [SerializeField] private float _maxAngleY = 30;
    [SerializeField] private float _speedPointer = 30;
    [Space]
    [SerializeField] private int _countBulletToOverHeating = 30;
    [SerializeField] private float _heatingTime = 30;
    [Space]
    [SerializeField] private float _coolingSpeed = 2;
    [SerializeField] private float _timeToOverHeating = 2;
    [Space]
    [SerializeField] private Renderer _bodyRenderer;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _lineRendererMultiply = 40;

    private Color _startColor;

    private bool _canShot = true;
    private int _bulletCounter;
    private float _heatingTimer;
    private bool _isShoting;

    private Quaternion _startRotation;
    public float _nowAngleX = 0;
    public float _nowAngleY = 0;

    public Vector3 _targetEulerAngle;
    public float transformYangle;
    public float angle;
    
    protected override void Start()
    {
        base.Start();
        _targetRotation = _startRotation = _rotateTransform.rotation;
        _startColor = _bodyRenderer.material.color;
    }

    protected override void Update()
    {
        base.Update();

        if (_isShoting) Shot(1);
        else
            _heatingTimer = Mathf.Lerp(_heatingTimer, 0, _coolingSpeed * Time.deltaTime);

        _isShoting = false;
        _bodyRenderer.material.color = Color.Lerp(_startColor, Color.red * 0.7f, Mathf.InverseLerp(0, _timeToOverHeating, _heatingTimer));
    }

    public void SetRotateSpeed(float speed) => _speedRotate = speed;

    public override void OnDown(Vector2 value)
    {
        _lineRenderer.enabled = true;
    }

    public override void OnPressed(Vector2 value)
    {
        NewRotate(value);

        //Rotate(value);
        _isShoting = true;
        _heatingTimer += Time.deltaTime;

        _lineRenderer.SetPosition(0, _spawn.position);
        _lineRenderer.SetPosition(1, _spawn.position + _spawn.forward * _lineRendererMultiply);
    }

    public override void OnUp(Vector2 value)
    {
        _lineRenderer.enabled = false;
    }

    public override bool Shot(float multiply)
    {
        if (_canShot && _timer > _shotPeriod)
        {
            if (base.Shot(multiply) == false) return false; 
            if (_heatingTimer >= _timeToOverHeating) StartCoroutine(OverHeat());
        }
        return true;
    }

    public override void AimShot(float multiply)
    {
        base.Shot(multiply);
    }

    private void NewRotate(Vector2 value)
    {
        _nowAngleX += value.y * _speedPointer * Time.deltaTime;
        _nowAngleY += value.x * _speedPointer * Time.deltaTime;

        _nowAngleX = Mathf.Clamp(_nowAngleX, - _maxAngleX, _maxAngleX);
        _nowAngleY = Mathf.Clamp(_nowAngleY, - _maxAngleY, _maxAngleY);

        _targetRotation = _startRotation;
        _targetRotation *= Quaternion.AngleAxis(-_nowAngleX, Vector3.right);
        _targetRotation *= Quaternion.AngleAxis(_nowAngleY, _rotateTransform.InverseTransformDirection(Vector3.up));
    }

    protected override void Rotate(Vector2 value)
    {
        //Quaternion rotationX = Quaternion.AngleAxis(value.x * _maxAngleY, _rotateTransform.InverseTransformDirection(Vector3.up));
        //Quaternion rotationY = Quaternion.AngleAxis(value.y * _maxAngleX, Vector3.right);
        //_targetRotation = rotationX * rotationY;

        Quaternion rotationX = Quaternion.AngleAxis(value.x * _speedPointer * Time.deltaTime, _rotateTransform.InverseTransformDirection(Vector3.up));
        Quaternion rotationY = Quaternion.AngleAxis(-value.y * _speedPointer * Time.deltaTime, Vector3.right);
        _targetRotation *= rotationX * rotationY;

        //Vector3 targetEuler = _targetRotation.eulerAngles;
        //_targetEulerAngle = targetEuler;

        //targetEuler.x = ClampedEulerAngl(targetEuler.x, _maxAngleX, transform.rotation.x);
        //targetEuler.y = ClampedEulerAngl(targetEuler.y, _maxAngleY, transform.rotation.y);
        //_targetRotation = Quaternion.Euler(targetEuler);
    }

    private float ClampedEulerAngl(float angle, float maxAngl, float transformOffset)
    {
        float clampedTransformAngle = Mathf.Clamp(
            TransformAngle(angle),
            TransformAngle(transformOffset - maxAngl),
            TransformAngle(transformOffset + maxAngl));
        return ReversTransformAngle(clampedTransformAngle);
    }

    private float TransformAngle(float angle)
    {
        if(angle > 180) angle = -360 + angle;
        return angle;
    }

    private float ReversTransformAngle(float angle)
    {
        return (360 + angle) % 360;
    }

    private IEnumerator OverHeat()
    {
        _canShot = false;
        Color _startColor = _bodyRenderer.material.color;
        _bodyRenderer.material.color = Color.red * 0.7f;

        yield return new WaitForSeconds(_heatingTime);

        _heatingTimer = 0;
        _canShot = true;
        _bodyRenderer.material.color = _startColor;
    }
}
