using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    [SerializeField] private TrajectoryRenderer trajectoryRenderer;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float throwForceMultiplier = 0.1f;
    [SerializeField] private float baseForce = 10f;
    [SerializeField] private float timeStep = 0.1f;
    [SerializeField] private int stepsCount = 30;
    
    private TrajectoryCalculator trajectoryCalculator;
    private Vector3 initialMousePos;
    private bool isDragging = false;

    private void Awake() => trajectoryCalculator = new TrajectoryCalculator();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePos = Input.mousePosition;
            isDragging = true;
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            var dragVector = initialMousePos - Input.mousePosition;
            var velocity = transform.forward * baseForce + transform.right * (dragVector.x * throwForceMultiplier) + transform.up * (dragVector.y * throwForceMultiplier);
            List<Vector3> points = trajectoryCalculator.CalculateTrajectory(spawnPoint.position, velocity, timeStep, stepsCount);
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (Physics.Linecast(points[i], points[i + 1], out RaycastHit hit))
                {
                    points[i + 1] = hit.point;
                    points.RemoveRange(i + 2, points.Count - (i + 2));
                    break;
                }
            }
            trajectoryRenderer.ShowTrajectory(points);
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            var dragVector = initialMousePos - Input.mousePosition;
            var velocity = transform.forward * baseForce + transform.right * (dragVector.x * throwForceMultiplier) + transform.up * (dragVector.y * throwForceMultiplier);
            var ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            var rb = ball.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = velocity;
            trajectoryRenderer.ClearTrajectory();
            isDragging = false;
        }
    }
}
