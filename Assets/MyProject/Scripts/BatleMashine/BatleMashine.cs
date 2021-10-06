using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatleMashine : Person
{
    [Header("BatleMashine")]
    [SerializeField] protected float _speedBullet = 3;
    [SerializeField] public float _shotPeriod = 3;
    [SerializeField] protected float _speedRotate = 4;
    [Space]
    [SerializeField] protected Transform _spawn;
    [SerializeField] protected GameObject _prefab;
    [SerializeField] public Transform _rotateTransform;
    [Space]
    [SerializeField] private AimBatleMashine aim;

    public Quaternion _targetRotation;
    protected float _timer = 0;

    public bool isPlayerControle { get; private set; }
    public Vector3 startForward { get; private set; }

    protected virtual void Start()
    {
        startForward = transform.forward;
    }

    protected override void Update()
    {
        if(IsActive) base.Update();

        _rotateTransform.rotation = Quaternion.Lerp(_rotateTransform.rotation, _targetRotation, _speedRotate * Time.deltaTime);
        _timer += Time.deltaTime;
    }   

    protected override void SetCameraParametrs()
    {
        _cameraMove.SetPosition(transform.position, _rotateTransform.rotation);
    }

    public virtual bool Shot(float multiply)
    {
        if (_timer <= _shotPeriod) return false;

        GameObject prefab = Instantiate(_prefab, _spawn.position, _spawn.rotation);
        prefab.GetComponent<Rigidbody>().velocity = _spawn.forward * _speedBullet * multiply;
        _timer = 0;
        return true;
    }

    public virtual void AimShot(float multiply) { }

    protected virtual void Rotate(Vector2 value)
    {
        Vector3 direction = new Vector3(value.x, 0, value.y) * -1;
        _targetRotation = Quaternion.LookRotation(direction);
    }

    public override void Activate()
    {
        base.Activate();
        aim.DeActivateAim();
    }

    public override void DeActivate()
    {
        base.DeActivate();
        enabled = true;
        aim.ActivateAim();
    }
}
