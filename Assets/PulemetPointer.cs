using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulemetPointer : MonoBehaviour
{
    [SerializeField] private float _maxDistanceRay = 40;

    [SerializeField] private Transform _spawn;
    [SerializeField] private Camera _mainCamera;

    private Vector3 pointerPos;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 pointerPosition;

        RaycastHit raycastHit;
        Ray ray = new Ray();
        ray.origin = _spawn.position;
        ray.direction = _spawn.forward;
        Physics.Raycast(ray, out raycastHit);

        pointerPosition = ray.GetPoint(_maxDistanceRay);
        //if (raycastHit.transform) pointerPosition = raycastHit.point;

        transform.position = _mainCamera.WorldToScreenPoint(pointerPosition);
        pointerPos = pointerPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(pointerPos, Vector3.one);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(_spawn.position, _spawn.forward * _maxDistanceRay);
    }
}
