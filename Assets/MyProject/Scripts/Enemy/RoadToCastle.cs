using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadToCastle : MonoBehaviour
{
    public List<RoadPoint> roadPoints;

    public RoadPoint GetNextPoint(RoadPoint roadPoint, out float nextPointScale)
    {
        nextPointScale = roadPoint.transform.localScale.x;

        int thisIndex = roadPoints.IndexOf(roadPoint);
        int nextIndex = thisIndex + 1;
        if (nextIndex == roadPoints.Count) return roadPoint;

        nextPointScale = roadPoints[nextIndex].transform.localScale.x;
        return roadPoints[nextIndex];
    }
}
