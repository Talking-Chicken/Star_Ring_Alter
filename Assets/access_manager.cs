using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class access_manager : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerBackpack playerBackpack;
    public int access_level;
    string[] parts;
    void Start()
    {
        playerBackpack = FindObjectOfType<PlayerBackpack>();
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("sec_station_checked"))
            {

                if (parts[2].Equals("TRUE"))
                {


                    access_level = 2;
                    break;
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Item accessCard = playerBackpack.getItem("access card");
        
        if (accessCard != null)
        {
            access_level = accessCard.GetComponent<AccessCard>().level;
            
                    }
    }
           
}
