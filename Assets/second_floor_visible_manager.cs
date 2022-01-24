using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class second_floor_visible_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] second_floor;
    public static bool player_at_second_floor=true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player_at_second_floor==true)
        {
            for (int i = 0; i < second_floor.Length; i++)
            {
                second_floor[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else 
        {
            for (int i = 0; i < second_floor.Length; i++)
            {
                second_floor[i].GetComponent<SpriteRenderer>().enabled=false;
            }
        }
    }
}
