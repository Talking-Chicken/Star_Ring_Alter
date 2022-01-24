using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffee_bean_console : InteractiveObj
{
    // Start is called before the first frame update
    public GameObject coffee_cover;
    Animator m_Animator;
    void Start()
    {
        m_Animator = coffee_cover.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact()
    {
      
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("coffee_stay_down")) //&& m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            m_Animator.Play("coffee_go_up");
        }
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("stay_up")) //&& m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            m_Animator.Play("coffee_go_down");
        }
    }
}
