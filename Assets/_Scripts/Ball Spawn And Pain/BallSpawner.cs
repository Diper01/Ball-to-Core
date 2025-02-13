using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public SpotManager spotManager;
    
    public float sphereRadius = 3f;
    private IBallColorStrategy iBallColorStrategy;
    public List<GameObject> spawnedBalls = new();

    [ContextMenu("BallSpawner")]
    public void MyStart()
    {
        iBallColorStrategy = new SpotStrategy(spotManager);
        SpawnBallsOnSphere(sphereRadius);
    }

    public void SpawnBallsOnSphere(float radius)
    {
        ClearList();

        float phi = Mathf.PI * (3 - Mathf.Sqrt(5));
        int ballCount = Mathf.RoundToInt(4 * Mathf.PI * radius * radius);
        var newSphere = new GameObject("Sphere");
        for (int i = 1; i < ballCount - 1; i++)
        {
            float y = 1 - (i / (float)(ballCount - 1)) * 2;
            float r = Mathf.Sqrt(1 - y * y);
            float theta = phi * i;
            float x = Mathf.Cos(theta) * r;
            float z = Mathf.Sin(theta) * r;
            var spawnPosition = new Vector3(x, y, z) * radius + transform.position;

            var gm = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            var newBall = gm.AddComponent<Ball>();
            newBall.Initialize(theta, x, y, z);
            newBall.transform.parent = newSphere.transform;
            iBallColorStrategy.ChangeColor(newBall);
            spawnedBalls.Add(newBall.gameObject);
        }
    }

    public void ClearList()
    {
        foreach (var ball in spawnedBalls)
        {
            if (ball == null) continue;
            if (Application.isPlaying)
                Destroy(ball);
            else
                DestroyImmediate(ball);
        }
        spawnedBalls.Clear();
    }
}