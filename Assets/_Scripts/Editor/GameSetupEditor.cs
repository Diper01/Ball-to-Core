using UnityEngine;
using UnityEditor;

public class GameSetupEditor : EditorWindow
{
    private BallSpawner ballSpawner;
    private SpotManager spotManager;
    private bool isSpotVisible = true;

    [MenuItem("Custom Window/Game Setup Window")]
    public static void ShowWindow()
    {
        var window = GetWindow<GameSetupEditor>("Game Setup");

        window.ballSpawner = FindObjectOfType<BallSpawner>();
        window.spotManager = FindObjectOfType<SpotManager>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Game Setup Controls", EditorStyles.boldLabel);

        ballSpawner = (BallSpawner)EditorGUILayout.ObjectField("Ball Spawner", ballSpawner, typeof(BallSpawner), true);

        if (ballSpawner != null)
        {
            ballSpawner.sphereRadius = EditorGUILayout.FloatField("Sphere Radius", ballSpawner.sphereRadius);

            if (GUILayout.Button("Spawn Balls"))
            {
                ballSpawner.MyStart();
                ballSpawner.SpawnBallsOnSphere(ballSpawner.sphereRadius);
            }
        }

        EditorGUILayout.Space();


        spotManager = (SpotManager)EditorGUILayout.ObjectField("Spot Manager", spotManager, typeof(SpotManager), true);

        if (spotManager != null)
        {
            spotManager.spotRadius = EditorGUILayout.FloatField("Spot Radius", spotManager.spotRadius);

            if (GUILayout.Button("Generate Spots"))
            {
                spotManager.GenerateSpotGroups();
                //spotManager.SpawnSpotMarkers(); if u want to see spot as a balls
                ballSpawner.MyStart();
                ballSpawner.SpawnBallsOnSphere(ballSpawner.sphereRadius);
                spotManager.ClearListOfList();
            }
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Show/Hide Spot"))
        {
            foreach (var spot in GameObject.FindGameObjectsWithTag("SpotMarker"))
            {
                spot.gameObject.GetComponent<MeshRenderer>().enabled = isSpotVisible;
            }

            isSpotVisible = !isSpotVisible;
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Clear Scene"))
        {
            ClearScene();
        }
    }

    private void ClearScene()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Ball"))
        {
            DestroyImmediate(obj);
        }

        foreach (var obj in GameObject.FindGameObjectsWithTag("SpotMarker"))
        {
            DestroyImmediate(obj);
        }

        if (ballSpawner != null)
        {
            ballSpawner.ClearList();
        }

        if (spotManager != null)
        {
            spotManager.ClearList();
        }

        Debug.Log("Scene cleared");
    }
}