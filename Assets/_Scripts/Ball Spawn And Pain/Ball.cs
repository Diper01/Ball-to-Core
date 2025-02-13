using System;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int groupID;
    [HideInInspector] public float theta;
    [HideInInspector] public float x;
    [HideInInspector] public float y;
    [HideInInspector] public float z;
    [HideInInspector] public List<Vector3> assignedSpots = new();
    
    private BallClusterDetector clusterManager;
    public MaterialEnum _materialEnum;
    private static List<Ball> allBalls = new();
    private MeshRenderer _meshRenderer;
    private Rigidbody _rb;
    private SphereCollider _collider;
    public MaterialController materialController;


    private void Awake()
    {
        allBalls.Add(this);
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        materialController = FindObjectOfType<MaterialController>();
        clusterManager = FindObjectOfType<BallClusterDetector>();
    }

    private void OnDestroy()
    {
        allBalls.Remove(this);
    }

    public void Initialize(float theta, float x, float y, float z)
    {
        this.theta = theta;
        this.x = x;
        this.y = y;
        this.z = z;
        _meshRenderer = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    public void AssignSpot(Vector3 spot)
    {
        if (!assignedSpots.Contains(spot))
        {
            assignedSpots.Add(spot);
        }
    }

    public void DeleteMe()
    {
        _rb.isKinematic = false;
        _collider.isTrigger = true;
    }

    public void ChangeColor(MaterialEnum color)
    {
        if (_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            if (_meshRenderer == null)
            {
                return;
            }
        }

        _materialEnum = color;
        
        GetComponent<Renderer>().material = materialController.GetMaterialByEnum(color);
    }

    //test method
   /* private void OnMouseDown()
    {
        if (clusterManager != null)
        {
            clusterManager.DeleteGroup(groupID);
        }
    }*/

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Shooter") && clusterManager != null )
        {
            clusterManager.DeleteGroup(groupID);
        }
    }
}