using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vending_manager : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] pos_list;
    int i = 0;
    float dist;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cat_icon;
    int loop_count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (loop_count>=3) { loop_count = 0;cat_icon.SetActive(true); }
        
            dist = Vector2.Distance(player.transform.position, pos_list[i].position);
        Debug.Log("loopcount:" + loop_count);
        if (dist<1.5f) {
                i = i + 1;

            }
            if (dist > 6)
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
