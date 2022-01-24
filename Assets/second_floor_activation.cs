using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class second_floor_activation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] second_floor_gameobject;
    private bool stair_active;
    void Update()
    {
        if (stair_active)
        {
         
            active_second_floor();
        }
        else
        {
            
            deactive_second_floor();
        }
        Debug.Log(GetComponentInChildren<SpriteRenderer>().sortingOrder);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        Collider2D triggerbox = GetComponent<BoxCollider2D>();

        if (other.transform.position.y < triggerbox.bounds.center.y)
        {
            stair_active = true;
        
            active_second_floor();
        }
        else
        {
            stair_active = false;
          
            deactive_second_floor();
        }
    }
    void deactive_second_floor()
    {
        for (int j = 0; j < second_floor_gameobject.Length; j++)
        {
            second_floor_gameobject[j].SetActive(false);
        }
    }
    void active_second_floor()
    {
        for (int j = 0; j < second_floor_gameobject.Length; j++)
        {
            second_floor_gameobject[j].SetActive(true);
        }
    }
}
