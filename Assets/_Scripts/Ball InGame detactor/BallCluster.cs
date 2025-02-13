using System.Collections.Generic;
using UnityEngine;

public class BallCluster : MonoBehaviour
{
    public float proximityThreshold = 2f;
    private int currentGroupId = 0;
    private Dictionary<int, List<Ball>> groupedBalls = new();

    private void Start()
    {
        AssignBallGroups();
    }

    private void AssignBallGroups()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        List<Ball> allBalls = new List<Ball>();

        foreach (var obj in balls)
        {
            allBalls.Add(obj.GetComponent<Ball>());
        }

        HashSet<Ball> visited = new HashSet<Ball>();

        foreach (var ball in allBalls)
        {
            if (!visited.Contains(ball))
            {
                List<Ball> cluster = new List<Ball>();
                FindCluster(ball, allBalls, cluster, visited);

                foreach (var b in cluster)
                {
                    b.groupID = currentGroupId;
                }

                groupedBalls[currentGroupId] = new List<Ball>(cluster); // Додаємо групу в словник
                currentGroupId++;
            }
        }
    }

    private void FindCluster(Ball ball, List<Ball> allBalls, List<Ball> cluster, HashSet<Ball> visited)
    {
        Stack<Ball> stack = new Stack<Ball>();
        stack.Push(ball);
        visited.Add(ball);

        while (stack.Count > 0)
        {
            Ball current = stack.Pop();
            cluster.Add(current);

            Collider[] neighbors = Physics.OverlapSphere(current.transform.position, proximityThreshold);
            foreach (Collider col in neighbors)
            {
                Ball neighbor = col.GetComponent<Ball>();
                if (neighbor != null && !visited.Contains(neighbor))
                {
                    stack.Push(neighbor);
                    visited.Add(neighbor);
                }
            }
        }
    }
}
