using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMoverToTargets : MonoBehaviour
{
    [System.Serializable]
    private class TargetForCamera 
    {
        public Transform Target;
        public float SpeedToTarget = 1;
        public float LerpToTarget = 1;
        public float RotateSpeed = 1;

        public UnityEvent OnComeTargetEvent;
    }

    [SerializeField] private bool _startWithExcurtion = true;
    [SerializeField] private MiniMassage _messegeStart;
    public int _nextTargetIndex = 0;
    [Space]
    [SerializeField] private CameraMove _cameraMove;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private UnityEvent NoStartEvent;
    [Space]
    [SerializeField] private TargetForCamera[] _targetsForCamera;

    private TargetForCamera _nowTargetToCamera;

    private bool _moveToTarget = false;

    private void Start()
    {
        if (_startWithExcurtion == false)
        {
            NoStartEvent.Invoke();
            DestroyExcurtion();
            return;
        }

        MoveNext();
        FindObjectOfType<JoystickManager>().HideJoystick();
        _cameraMove.enabled = false;

        if (_messegeStart != null) _messegeStart.ShowMessege();
    }

    private void Update()
    {
        MoveCamera();

        if (_moveToTarget && Vector3.Distance(_nowTargetToCamera.Target.position, _cameraMove.transform.position) < 1)
        {
            if (_nextTargetIndex == _targetsForCamera.Length)
            {                
                return;
            }
            _moveToTarget = false;
            _nowTargetToCamera.OnComeTargetEvent.Invoke();
        }
    }

    private void MoveCamera()
    {
        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _nowTargetToCamera.Target.position, 
            Time.deltaTime * _nowTargetToCamera.LerpToTarget);
        _cameraTransform.position = Vector3.MoveTowards(_cameraTransform.position, _nowTargetToCamera.Target.position,
            Time.deltaTime * _nowTargetToCamera.SpeedToTarget);

        _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, _nowTargetToCamera.Target.rotation,
            Time.deltaTime * _nowTargetToCamera.RotateSpeed);
    }

    public void MoveNext()
    {
        _nowTargetToCamera = _targetsForCamera[_nextTargetIndex];

        _nextTargetIndex++;
        _moveToTarget = true;
    }

    public void DestroyExcurtion()
    {
        Destroy(gameObject);
    }
}
