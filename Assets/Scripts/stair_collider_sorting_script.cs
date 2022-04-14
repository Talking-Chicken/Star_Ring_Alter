using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stair_collider_sorting_script : MonoBehaviour
{
    // Start is called before the first frame update
    private bool stair_active=false;


    public GameObject stair_blocker;
    public GameObject stair_blocker_2;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (stair_active)
        { active_stair();
           
        }
        else { deactive_stair();
           
        }
      //  Debug.Log(GetComponentInChildren<SpriteRenderer>().sortingOrder);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      
            Collider2D triggerbox = GetComponent<PolygonCollider2D>();
      
        if (other.transform.position.y < triggerbox.bounds.center.y)
        {
            stair_active = true;
            active_stair();
           
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        Collider2D triggerbox = GetComponent<PolygonCollider2D>();

        if (other.transform.position.y < triggerbox.bounds.center.y)
        {
           
            stair_active = false;
            deactive_stair();

        }
    }

    void active_stair()
    {

        
        EdgeCollider2D[] edge;

        edge = GetComponents<EdgeCollider2D>();
        GetComponent<SpriteRenderer>().sortingOrder = GameObject.Find("main_character").GetComponent<SpriteRenderer>().sortingOrder-1;

        for (int i = 0; i < edge.Length; i++)
        {
            edge[i].enabled = true;

        }
        stair_blocker.SetActive(false);
        stair_blocker_2.SetActive(false);

    }
    void deactive_stair()
    {
       
        EdgeCollider2D[] edge;
        GetComponent<SpriteRenderer>().sortingOrder = GameObject.Find("main_character").GetComponent<SpriteRenderer>().sortingOrder + 1;

        edge = GetComponents<EdgeCollider2D>();

        for (int i = 0; i < edge.Length; i++)
        {
            edge[i].enabled = false;

        }
        stair_blocker.SetActive(true);
        stair_blocker_2.SetActive(true);
    }
  
}
