using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truck_radar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,new Vector3(-1,-1,0),10.0f);
       
        // If it hits something...
        if (hit.collider!= null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
        
    }
}
