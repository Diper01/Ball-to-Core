using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClusterDetector : MonoBehaviour
{
    public float proximityThreshold = 0.3f;
    private Dictionary<Color, List<List<Ball>>> colorClusters = new();
    private Dictionary<int, List<Ball>> groupedBalls = new();
    private int currentGroupId = 0;

    void Start()
    {
        StartCoroutine(DetectBallClusters());
    }

    IEnumerator DetectBallClusters()
    {
        yield return new WaitForSeconds(1);
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        Dictionary<Color, List<Ball>> colorGroups = new();

        foreach (var obj in balls)
        {
            var ball = obj.GetComponent<Ball>();
            if (ball == null) continue;
            var ballColor = ball.GetComponent<Renderer>().material.color;
            if (!colorGroups.ContainsKey(ballColor))
                colorGroups[ballColor] = new List<Ball>();
            colorGroups[ballColor].Add(ball);
        }

        foreach (var colorGroup in colorGroups)
            colorClusters[colorGroup.Key] = FindClusters(colorGroup.Value);

        PrintClusters();
    }

    List<List<Ball>> FindClusters(List<Ball> balls)
    {
        List<List<Ball>> clusters = new();
        HashSet<Ball> visited = new();

        foreach (var ball in balls)
        {
            if (visited.Contains(ball)) continue;
            List<Ball> cluster = new();
            DFS(ball, balls, cluster, visited);
            foreach (var b in cluster)
                b.groupID = currentGroupId;
            groupedBalls[currentGroupId] = new List<Ball>(cluster);
            clusters.Add(cluster);
            currentGroupId++;
        }
        return clusters;
    }

    private void DFS(Ball ball, List<Ball> allBalls, List<Ball> cluster, HashSet<Ball> visited)
    {
        Stack<Ball> stack = new();
        stack.Push(ball);
        visited.Add(ball);
        var parentRef = ball.transform.parent;

        while (stack.Count > 0)
        {
            Ball current = stack.Pop();
            cluster.Add(current);

            foreach (var neighbor in allBalls)
            {
                if (!visited.Contains(neighbor) &&
                    Vector3.Distance(current.transform.position, neighbor.transform.position) < proximityThreshold &&
                    neighbor.transform.parent == parentRef)
                {
                    stack.Push(neighbor);
                    visited.Add(neighbor);
                }
            }
        }
    }

    private void PrintClusters()
    {
        foreach (var color in colorClusters.Keys)
            Debug.Log($"Color: {color}, Groups: {colorClusters[color].Count}");
    }

    public void DeleteGroup(int groupID)
    {
        if (groupedBalls.ContainsKey(groupID))
        {
            List<Ball> ballsToRemove = new(groupedBalls[groupID]);
            Debug.Log($"Group ID: {groupID}");
            foreach (var ball in ballsToRemove)
                ball.DeleteMe();
            groupedBalls.Remove(groupID);
        }
    }
}
