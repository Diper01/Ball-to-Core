using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private void Awake() => lineRenderer = GetComponent<LineRenderer>();

    public void ShowTrajectory(List<Vector3> points)
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}