using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPerson : Person
{
    [Header("Tank")]
    [SerializeField] private Transform _boxTransform;
    [SerializeField] private BoxMove _boxMove;
    [Space]
    [SerializeField] private float _decraceSpeedCamera = 1;
    [SerializeField] private float _decracePositionCamera = 1.3f;
    [Space]
    [SerializeField] private float cameraCrushMultiply = 1;

    public Vector3 LastDirecton { get; private set; } = new Vector3(0, 0, 1);

    private Vector3 _startPositionCamera;
    private float _startRotateSpeedCamera;
    private float _startSpeedCamera;

    private void Start()
    {
        _startPositionCamera = _cameraSetup.vectorToCamera;
        _startRotateSpeedCamera = _cameraSetup.rotateSpeed;
        _startSpeedCamera = _cameraSetup.cameraMoveSpeed;
    }

    public override void OnDown(Vector2 value)
    {
        Time.timeScale = 0.35f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        _cameraSetup.rotateSpeed /= _decraceSpeedCamera;
        _cameraSetup.vectorToCamera /= _decracePositionCamera;
        _cameraSetup.cameraMoveSpeed /= Time.timeScale;

        _boxMove.OnDown(Vector3.zero);
    }

    public override void OnPressed(Vector2 value)
    {
        Vector3 direction = new Vector3(value.x, 0f, value.y) * -1;
        Vector3 invertDirection = _cameraMove.FromWorldToCamera(direction);
        LastDirecton = invertDirection;

        _boxMove.OnPressed(LastDirecton);
    }

    public override void OnUp(Vector2 value)
    {
        _cameraSetup.rotateSpeed = _startRotateSpeedCamera;
        _cameraSetup.vectorToCamera = _startPositionCamera;
        _cameraSetup.cameraMoveSpeed = _startSpeedCamera;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (value.magnitude < 0.25f) return;
        _boxMove.OnUp(LastDirecton);
    }

    protected override void SetCameraParametrs()
    {
        _cameraMove.SetPosition(_boxTransform.position, Quaternion.LookRotation(LastDirecton));
    }

    public void AddCrushCameraOnCollision(float inverseBounce)
    {
        if(IsActive)
            _cameraMove.CameraCrush.AddCrush(inverseBounce);
    }
}
