using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraectoryLine : MonoBehaviour
{
    [SerializeField] Transform _pointer;
    [SerializeField] LineRenderer[] _lineRenderers;

    public void SetPointPosition(Vector3 origin, Vector3 startSpeed)
    {
        Vector3[] points = new Vector3[30];

        int count = 0;
        for (int i = 0; i < points.Length; i++, count ++)
        {
            float time = i * 0.05f;

            points[i] = origin + startSpeed * time + Physics.gravity * time * time / 2;
            if (points[i].y < 0)
            {
                SetPositionToLine(origin, points[i] - origin);
                _pointer.position = points[i];
                break;
            }
        }
        //SetPositionAndCount(points, count);
    }

    public void OnOffLines(bool active)
    {
        if (_lineRenderers[0].enabled == active) return;

        _pointer.gameObject.SetActive(active);
        foreach (var item in _lineRenderers)
        {
            item.enabled = active;
        }
    }

    private void SetPositionAndCount(Vector3[] points, int count)
    {
        foreach (var item in _lineRenderers)
        {
            item.positionCount = count;
            item.SetPositions(points);
        }
    }

    private void SetPositionToLine(Vector3 origin, Vector3 direction)
    {
        foreach (var item in _lineRenderers)
        {
            item.SetPosition(0, origin);
            item.SetPosition(1, origin + direction);
        }
    }
}
