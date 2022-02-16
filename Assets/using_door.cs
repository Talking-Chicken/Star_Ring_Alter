using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class using_door : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject floor_name;
    private TMP_Text floor_text;
    void Start()
    {
        floor_text=floor_name.GetComponent<TMP_Text>();
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
            floor_text.text = thisDoor.floor_name;
            gameObject.transform.position = thisDoor.dest; //teleport player to new destination
            if (thisDoor.change_floor) {
                
                IsometricCharacterRenderer.foot_step_clip = thisDoor.floor_name; }

        }
    }
}
