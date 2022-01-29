using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class broken_door : MonoBehaviour
{
    Animator m_Animator;
    public GameObject door;
    public GameObject robot;

    [SerializeField] private GameObject talkingArea;
    void Start()
    {
        m_Animator = robot.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("patrol"))
        {
            door.SetActive(true);
            gameObject.SetActive(false);
            
            if (talkingArea != null)
                talkingArea.SetActive(true);
        }
    }
}
