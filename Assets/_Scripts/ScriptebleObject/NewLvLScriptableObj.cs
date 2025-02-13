using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball", menuName = "Ball/BallLvl")]
public class NewLvLScriptableObj : ScriptableObject
{
    public List<GameObject> spheres = new();
}