using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBound : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag.Equals("Projectile") || collider.GetComponent<Enemy>() != null) {
            Destroy(collider.gameObject);
        }
    }

    
}
