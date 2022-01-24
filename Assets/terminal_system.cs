using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminal_system : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject door;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("scan_complete")) //&& m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            //unlock door at end of animation clip scan
            m_Animator.SetTrigger("complete");
            door.GetComponent<door_control>().open = true;
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            m_Animator.SetTrigger("scan");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player"&& m_Animator.GetCurrentAnimatorStateInfo(0).IsName("scan"))
        {
            m_Animator.Play("no_pass");
        }
    }
}
