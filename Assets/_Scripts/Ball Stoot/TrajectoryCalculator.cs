using System.Collections.Generic;
using UnityEngine;

public class TrajectoryCalculator
{
    private float gravity = 9.81f;
    public List<Vector3> CalculateTrajectory(Vector3 startPosition, Vector3 initialVelocity, float timeStep, int stepsCount)
    {
        List<Vector3> trajectoryPoints = new();
        for (int i = 0; i < stepsCount; i++)
        {
            float t = i * timeStep;
            var position = startPosition + initialVelocity * t;
            position.y = startPosition.y + initialVelocity.y * t - 0.5f * gravity * t * t;
            trajectoryPoints.Add(position);
        }
        return trajectoryPoints;
    }
}