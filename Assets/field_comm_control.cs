using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class field_comm_control : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Animator anim;
    bool playonce = true;
    bool playonce1 = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (playonce&&dist <= 10 && !anim.GetCurrentAnimatorStateInfo(0).IsName("field_comm_opened")) 
        { anim.Play("field_comm_opening"); playonce = false; }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("field_comm_opened")) {
            playonce1 = true;
            playonce = true;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("field_comm_closed"))
        {
           
            playonce = true;
            playonce1 = true;

        }
        else if (playonce1&&dist > 10 && !anim.GetCurrentAnimatorStateInfo(0).IsName("field_comm_closed"))
        {
            anim.Play("field_comm_closing");
            playonce1 = false;
        }

    }
}
