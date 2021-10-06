using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CameraSetup
{
    public float moveSpeed = 8;
    public float rotateSpeed = 8;
    public float cameraMoveSpeed = 8;
    public Vector3 vectorToCamera;
    public Vector3 cameraAngle = new Vector3(45, 0, 0);

    public UnityEvent UnityEvent;
}

public class CameraMove : MonoBehaviour
{

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CameraSetup _activeSetup;
    [SerializeField] private CameraCrush _cameraCrush;
    public CameraCrush CameraCrush { get => _cameraCrush; }

    public bool IsLerpMoving = true;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Quaternion _newCameraAngle;

    void LateUpdate()
    {
        if (IsLerpMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _activeSetup.moveSpeed);
        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _activeSetup.moveSpeed);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _activeSetup.rotateSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)) _cameraCrush.AddCrush(1);

        if (_cameraCrush.IsCrush) return;
        _cameraTransform.localRotation = Quaternion.Lerp(_cameraTransform.localRotation, _newCameraAngle, 5 * Time.deltaTime);
        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _activeSetup.vectorToCamera, _activeSetup.cameraMoveSpeed * Time.deltaTime);

    }

    public void SetSetup(CameraSetup cameraSetup, bool isStrtSetup)
    {
        _activeSetup = cameraSetup;
        if (isStrtSetup) _activeSetup = new CameraSetup();

        _newCameraAngle = Quaternion.Euler(_activeSetup.cameraAngle);
    }

    public void SetPosition(Vector3 targetPosition, Quaternion targetRotation)
    {
        _targetPosition = targetPosition;
        _targetRotation = targetRotation;
    }

    public Vector3 FromWorldToCamera(Vector3 world)
    {
        float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
        Quaternion cameraRotation = _cameraTransform.rotation;
        _cameraTransform.rotation = Quaternion.identity;
        _cameraTransform.rotation = Quaternion.LookRotation(world);
        _cameraTransform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);

        Vector3 toCamera = _cameraTransform.forward * world.magnitude;
        _cameraTransform.rotation = cameraRotation;
        return toCamera;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_targetPosition, Vector3.one);
    }
}

