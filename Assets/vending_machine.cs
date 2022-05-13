using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vending_machine : InteractiveObj
{
    // Start is called before the first frame update
    bool active;
    string[] parts;
    bool once=true;
    [SerializeField]GameObject[] icons;
    void Start()
    {
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("spy_terminal") && parts[2].Equals("TRUE"))
            {

                active = true;

                break;
            }
            else { active = false; }


        }
    }

    // Update is called once per frame
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        if (active==false) { player.talkToSelf("Investigate.spy_terminal"); 
            
            for (var i = 0; i < random_conversation.lines.Length; i++)
            {
                parts = random_conversation.lines[i].Split(',');
                parts[0] = parts[0].Replace("\r", "");

                if (parts[0].Equals("spy_terminal"))
                {
                    random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "TRUE" + "," + parts[3];

                    active = true;

                    break;
                }
                


            }

        }
        else {
            player.talkToSelf("Investigate.spy_terminal_2");
        }
        if (active&&once) { once = false;
            for (int i=0;i<icons.Length;i++) {

                icons[i].SetActive(true);
            }
        }
    }
}
