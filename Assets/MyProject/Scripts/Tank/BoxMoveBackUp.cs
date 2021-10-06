using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxMoveBackUp : MonoBehaviour {

    [SerializeField] private Transform _boxTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _aim;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Joystick _joystick;

    [SerializeField] private float _forceMultiplier = 5f;
    [SerializeField] private float _aimDirectionMultiplier = 8f;

    public float Charge = 1f;
    [SerializeField] private Slider ChargeSlider;

    public Vector3 LastDirecton { get; private set; }
    [SerializeField] private Transform _cameraCentr;

    private void Update() {
        Charge += Time.deltaTime;
        Charge = Mathf.Clamp(Charge, 0f, 1f);
        ChargeSlider.value = Charge;
    }

    private void Start() {
        _joystick.OnDownEvent.AddListener(OnDown);
        _joystick.OnPressedEvent.AddListener(OnPressed);
        _joystick.OnUpEvent.AddListener(OnUp);
    }

    void OnDown(Vector2 touchPosition) {
        _lineRenderer.enabled = true;
        _aim.gameObject.SetActive(true);
        Time.timeScale = 0.35f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    void OnPressed(Vector2 touchPosition) {
        Vector3 aimOffset = FromWorldToCamera(new Vector3(_joystick.Value.x, 0f, _joystick.Value.y)) * _aimDirectionMultiplier * Charge;
        Vector3 pointerPosition = _boxTransform.position - aimOffset;
        _aim.transform.position = pointerPosition;
        _lineRenderer.SetPosition(0, _boxTransform.position);
        _lineRenderer.SetPosition(1, pointerPosition);
    }

    void OnUp(Vector2 touchPosition) {

        Vector3 inputValue = new Vector3(_joystick.Value.x, 0f, _joystick.Value.y);
        Vector3 velocity = -1 * inputValue * _forceMultiplier;

        Vector3 invertVelosity = FromWorldToCamera(velocity);
        LastDirecton = invertVelosity;
        //FindObjectOfType<CameraMove>().targetRotation = 

        _rigidbody.velocity = invertVelosity * Charge;
        Charge -= inputValue.magnitude;

        _lineRenderer.enabled = false;
        _aim.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

    }

    private Vector3 FromWorldToCamera(Vector3 world)
    {
        float angle = Vector3.SignedAngle(Vector3.forward, _cameraCentr.forward, Vector3.up);
        Transform localTransform = new GameObject().transform;
        localTransform.rotation = Quaternion.identity;
        localTransform.rotation = Quaternion.LookRotation(world);
        localTransform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);

        return localTransform.forward * world.magnitude;
    }


}
