using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balista : BatleMashine
{
    [Header("Balista")]
    [SerializeField] private float _maxAngleY = 45;
    [Range(0, 1)]
    [SerializeField] private float _minMultiply = 0.4f;
    public float MinMultiply { get => _minMultiply; }

    [SerializeField] private Vector3 _vectToCameraInPricel = Vector3.one;
    private Vector3 _startVectToCamera;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private TraectoryLine traectoryLine;

    public override void OnDown(Vector2 value)
    {
        TimeScale.Singltone.ChangeScaleOnTime(0.35f, 4f);
        traectoryLine.OnOffLines(true);
        _startVectToCamera = _cameraSetup.vectorToCamera;
        _cameraSetup.vectorToCamera = _vectToCameraInPricel;
    }

    public override void OnPressed(Vector2 value)
    {
        float clampedmulMultiply = Mathf.Clamp(value.magnitude, _minMultiply, 1);
        traectoryLine.SetPointPosition(_spawn.position, _spawn.forward * clampedmulMultiply * _speedBullet);

        Rotate(value);
    }

    public override void OnUp(Vector2 value)
    {
        TimeScale.Singltone.NormalizeScale();
        traectoryLine.OnOffLines(false);
        _cameraSetup.vectorToCamera = _startVectToCamera;

        Shot(value.magnitude);
    }

    protected override void SetCameraParametrs()
    {
        _cameraMove.SetPosition(transform.position, _rotateTransform.rotation);
    }

    public override bool Shot(float multiply)
    {
        float clampedMultiply = Mathf.Clamp(multiply, _minMultiply, 1);

        if (base.Shot(clampedMultiply)) animator.SetTrigger("Shot");
        return true;
    }

    public new Arrow AimShot(float multiply)
    {
        GameObject prefab = Instantiate(_prefab, _spawn.position, _spawn.rotation);
        float clampedmulMultiply = Mathf.Clamp(multiply, _minMultiply, 1);
        prefab.GetComponent<Rigidbody>().velocity = _spawn.forward * _speedBullet * clampedmulMultiply;

        animator.SetTrigger("Shot");
        return prefab.GetComponent<Arrow>();
    }

    protected override void Rotate(Vector2 value)
    {
        float joystickAngle = Vector2.Angle(value, Vector2.down);
        float inverseAngle = Mathf.InverseLerp(0, 90, joystickAngle);
        if(joystickAngle > 90)
        {
            inverseAngle = Mathf.InverseLerp(180, 90, joystickAngle);
        }

        Quaternion startRotation = Quaternion.LookRotation(startForward);
        Quaternion newAngle = Quaternion.AngleAxis(-1 * _maxAngleY * inverseAngle * Mathf.Sign(value.x), Vector3.up);
        _targetRotation = startRotation * newAngle;
    }
}
