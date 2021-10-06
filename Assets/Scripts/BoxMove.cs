using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxMove : MonoBehaviour {

    [Header("BoxMove")]
    [SerializeField] private Transform _boxTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _aim;
    [SerializeField] private LineRenderer _lineRenderer;
    [Space]
    [SerializeField] private float _forceMultiplier = 5f;
    [SerializeField] private float _aimDirectionMultiplier = 8f;
    [SerializeField] private float _timeToFullCharge = 1f;

    private float _charge = 1f;

    private void Start()
    {
        _rigidbody.centerOfMass = new Vector3(0f, 0.1f, 0f);
    }

    private void Update()
    {
        _charge += Time.deltaTime / _timeToFullCharge;
        _charge = Mathf.Clamp(_charge, 0f, 1f);
    }

    public void OnDown(Vector3 direction) 
    {
        _lineRenderer.enabled = true;
        _aim.gameObject.SetActive(true);
    }

    public void OnPressed(Vector3 direction) 
    {
        Vector3 aimOffset = direction * _aimDirectionMultiplier * _charge;
        Vector3 pointerPosition = _boxTransform.position + aimOffset;

        _aim.transform.position = pointerPosition;
        _lineRenderer.SetPosition(0, _boxTransform.position);
        _lineRenderer.SetPosition(1, pointerPosition);
    }

    public void OnUp(Vector3 direction) 
    {
        Vector3 velocity = direction * _forceMultiplier;

        _rigidbody.velocity = velocity * _charge;
        _charge -= velocity.magnitude;

        _lineRenderer.enabled = false;
        _aim.gameObject.SetActive(false);
    }
}
