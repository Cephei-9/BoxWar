using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : Person
{
    [Header("Navigation")]
    [SerializeField] private float _speedMove = 3;
    [SerializeField] private float _speedRotate = 3;
    [SerializeField] private Vector3 _minValue;
    [SerializeField] private Vector3 _maxValue;

    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private Quaternion _targetRotation;

    private Vector3 _lastPosition;
    private Person _lastPerson;

    protected void Start()
    {
        _startPosition =_targetPosition = transform.position;
        _targetRotation = Quaternion.identity;
    }

    protected override void Update()
    {
        base.Update();

        transform.position = Vector3.Lerp(transform.position, _targetPosition, _speedMove * 2 * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _speedRotate * Time.deltaTime);
    }

    public override void OnPressed(Vector2 value)
    {
        Vector3 direction = new Vector3(value.x, 0, value.y);
        //Vector3 inversDirection = _cameraMove.FromWorldToCamera(direction);
        //Rotate(value.x);
        SetTargetPosition(Vector3.Project(direction, Vector3.forward));
        SetTargetPosition(Vector3.Project(direction, Vector3.right));
    }

    private void SetTargetPosition(Vector3 direction)
    {
        Vector3 nowPosition = _targetPosition;
        Vector3 newPosition = nowPosition + direction * _speedMove;

        if (newPosition.x < _maxValue.x && newPosition.z < _maxValue.z 
            && 
            newPosition.x > _minValue.x && newPosition.z > _minValue.z)
        {
            _targetPosition += direction * _speedMove;
        }
    }

    private void Rotate(float valueX)
    {
        _targetRotation *= Quaternion.AngleAxis(valueX * _speedRotate, Vector3.up);
    }

    protected override void SetCameraParametrs()
    {
        _cameraMove.SetPosition(_targetPosition, _targetRotation);
    }

    public void ReturnInLastPosition()
    {
        _targetPosition = _lastPosition;
    }

    public void ActiveLastPerson()
    {
        if (_lastPerson == null) return;
        _personManager.ChangePersonOnThis(_lastPerson);
    }

    public void Activate(Person lastPerson)
    {
         _lastPerson = lastPerson;

        TimeScale.Singltone.ChangeScale(0.35f);

        base.Activate();
    }

    public override void DeActivate()
    {
        _lastPosition = _targetPosition;
        _targetPosition = _startPosition;

        _personManager.SetActiveAllNavigationsInterface(false);

        TimeScale.Singltone.NormalizeScale();

        base.DeActivate();
    }
}
