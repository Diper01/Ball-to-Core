using UnityEngine;

public class TriggerDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger)
            Destroy(other.gameObject);
    }
    
}