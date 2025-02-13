using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private NewLvLScriptableObj[] levels;
    
    private void Start()
    {
        StartLvl(0); 
        //Application.targetFrameRate = 60; // just don't wanna make a build again
    }

    private void StartLvl(int lvl)
    {
        foreach (var obj2Spawn in levels[lvl].spheres)
        {
            Instantiate(obj2Spawn ,Vector3.zero, Quaternion.identity);
        }
    }


}