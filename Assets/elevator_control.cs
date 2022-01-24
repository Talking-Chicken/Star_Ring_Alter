using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator_control : InteractiveObj
{
    // Start is called before the first frame update
    public GameObject to_corridor;
    public GameObject to_roof_top;
    public GameObject tile_corridor;
    public GameObject tile_roof_top;
    public GameObject blocker;
    Animator m_Animator;
    private int state;//0=moving up 1=first floor 2=second floor 3=moving down
    void Start()
    {
        state = 1;
        m_Animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact()
    {
        if (state==1) {
            state = 0;
            m_Animator.Play("up");
            StartCoroutine(moving_up());
            tile_corridor.SetActive(false);
            tile_roof_top.SetActive(false);
            blocker.SetActive(true);
            to_corridor.SetActive(false);
            to_roof_top.SetActive(false);
        }
        if (state == 2)
        {
            state = 3;
            m_Animator.Play("down");
            StartCoroutine(moving_down());
            tile_corridor.SetActive(false);
            tile_roof_top.SetActive(false);
            blocker.SetActive(true);
            to_corridor.SetActive(false);
            to_roof_top.SetActive(false);
        }


    }
    IEnumerator moving_up()
    {
        
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
   
        m_Animator.Play("stop");
        blocker.SetActive(false);
        state = 2;
        tile_roof_top.SetActive(true);
        to_roof_top.SetActive(true);
    }
    IEnumerator moving_down()
    {

        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.

        m_Animator.Play("stop");
        blocker.SetActive(false);
        state = 1;
        tile_corridor.SetActive(true);
        to_corridor.SetActive(true);
    }
}
