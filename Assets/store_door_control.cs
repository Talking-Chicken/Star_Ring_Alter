using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store_door_control : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] GameObject door;
    [SerializeField] GameObject door1;
    bool once=true;
    bool once1=false;
    void Update()
    {
       
        if (Vector2.Distance(player.transform.position, this.transform.position) < 7) {
            if (once && door.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("front_door_still")) { once = false;
               // Debug.Log("yes");
                door.GetComponent<Animator>().Play("open");
                door1.GetComponent<Animator>().Play("open_right");
                once1 = true;
            }
         
        } else
        {
            if (once1&& door.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("stay_open"))
            {
                door.GetComponent<Animator>().Play("close");
                door1.GetComponent<Animator>().Play("close_right");
                once1 = false;
                once = true;
            }

        }
    }
}
