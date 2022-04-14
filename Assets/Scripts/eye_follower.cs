using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye_follower : MonoBehaviour
{
    // Start is called before the first frame update
  
    Animator animator;
    public Transform player;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = player.position - transform.position;
        float angle = Vector3.Angle(player.transform.position, this.transform.position);
        
        animator.Play(Mathf.FloorToInt(angle).ToString());
    }
}
