using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store_door_animation_manager : MonoBehaviour
{
    // Start is called before the first frame update
    Animator door_anime;
    public door_control door;
    bool once;
    bool once_1; 
   public GameObject player;
   
    void Start()
    {
        door_anime = GetComponent<Animator>();
         once = true;
        once_1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (door.open == true&&once) {
            door_anime.Play("store_door_open");
            once = false;
            once_1 = true;
        }
        Debug.Log("once"+once);
        Debug.Log("once1" + once_1);
        if (dist >10f&&once_1&& door.open ==false) { once = true; once_1 = false; door_anime.Play("store_door_close"); }
    }
}
