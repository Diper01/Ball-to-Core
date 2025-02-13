using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpotManager : MonoBehaviour
{
    public BallSpawner ballSpawner;

    public MaterialController _materialController;

    public float spotRadius = 0.5f;
    public float spawnSphereRadius = 3f;

    public List<SpotGroupConfig> spotConfigs = new();
    public GameObject spotMarkerPrefab;
    public List<GameObject> spawnedSpots = new();

    private List<List<Spot>> _spotGroups = new();

   

    private Dictionary<int, List<GameObject>> spotGroups = new();

    public void GenerateSpotGroups()
    {
        ClearList();
        int currentGroupID = 0;

        foreach (var config in spotConfigs)
        {
            List<Spot> newGroup = new();

            Vector3 firstSpot = GetRandomPointOnSphere();
            newGroup.Add(new Spot(firstSpot, config.materialEnum));

            for (int i = 1; i < config.spotCount; i++)
            {
                Vector3 newSpot = GenerateAdjacentSpot(newGroup);
                newGroup.Add(new Spot(newSpot, config.materialEnum));
            }

            _spotGroups.Add(newGroup);
            spotGroups[currentGroupID] = new List<GameObject>();

            currentGroupID++;
        }
    }

    ///visualization of spots
    public void SpawnSpotMarkers()
    {
        foreach (var group in _spotGroups)
        {
            foreach (var spot in group)
            {
                if (spotMarkerPrefab != null)
                {
                    GameObject marker = Instantiate(spotMarkerPrefab, spot.position, Quaternion.identity);
                    marker.transform.position = transform.position + spot.position;
                    marker.transform.localScale = Vector3.one * (spotRadius * 2f);
                    marker.GetComponent<Renderer>().sharedMaterial =
                        _materialController.GetMaterialByEnum(spot.materialEnum);
                    spawnedSpots.Add(marker);
                }
            }
        }
    }

    public void ClearList()
    {
        spawnedSpots.Clear();
    }

    public void ClearListOfList()
    {
        _spotGroups.Clear();
    }


    private Vector3 GenerateAdjacentSpot(List<Spot> group)
    {
        var baseSpot = group[Random.Range(0, group.Count)].position;

        for (int attempt = 0; attempt < 10; attempt++)
        {
            float theta = Random.Range(0f, Mathf.PI * 2);
            float phi = Mathf.Acos(2 * Random.value - 1);
            float x = Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = Mathf.Cos(phi);

            var candidate = baseSpot + new Vector3(x, y, z) * (spawnSphereRadius * 0.1f);

            //check Distance
            if (group.Exists(s => Vector3.Distance(s.position, candidate) < spawnSphereRadius * 0.1f)) ;
            {
                return candidate;
            }
        }

        return baseSpot;
    }


    private Vector3 GetRandomPointOnSphere()
    {
        float phi = Mathf.PI * (3 - Mathf.Sqrt(5));
        int i = Random.Range(1, 100);
        float y = 1 - (i / 100f) * 2;
        float r = Mathf.Sqrt(1 - y * y);
        float theta = phi * i;
        float x = Mathf.Cos(theta) * r;
        float z = Mathf.Sin(theta) * r;

        return new Vector3(x, y, z);
    }

   

    public List<Spot> GetAllSpots()
    {
        return _spotGroups.SelectMany(group => group).ToList();
    }
}

[System.Serializable]
public class SpotGroupConfig
{
    public int spotCount;
    public MaterialEnum materialEnum;
}

public class Spot
{
    public Vector3 position;
    public MaterialEnum materialEnum;


    public Spot(Vector3 position, MaterialEnum materialEnum)
    {
        this.position = position;
        this.materialEnum = materialEnum;
    }
}