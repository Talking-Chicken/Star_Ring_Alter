using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vending_manager : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] pos_list;
    int i = 0;
    Vector2 dist;
    [SerializeField] GameObject player; 
    int loop_count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("loop:"+loop_count);
        while(i<=pos_list.Length) {
       
            if (Vector2.Distance(player.transform.position,pos_list[i].position)<1) {
                i = i + 1;

            }
            if (Vector2.Distance(player.transform.position, pos_list[i].position) > 5)
            {
                i = 0;
                loop_count = 0;
            }

            if (i == pos_list.Length) 
            {
                loop_count++;
                i = 0;
            
            }
        }
    }
}
