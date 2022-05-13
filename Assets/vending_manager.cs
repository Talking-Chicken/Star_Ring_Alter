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
    public static bool clue;
    string[] parts;
    void Start()
    {
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("hint_checked") && parts[2].Equals("TRUE"))
            {

                clue = true;
                break;

            }
            else { clue = false; }
         

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (loop_count>=3 &&clue ) { loop_count = 0;cat_icon.SetActive(true); }
        
            dist = Vector2.Distance(player.transform.position, pos_list[i].position);
       
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
