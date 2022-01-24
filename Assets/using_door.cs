using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class using_door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collides w/ a door, teleport them to the spawn location of specified destination door
        if (other.gameObject.tag == "Door")
        {
            door thisDoor = other.gameObject.GetComponent<door>(); //get the "ChangeRoom" script of this door

            gameObject.transform.position = thisDoor.dest; //teleport player to new destination


        }
    }
}
