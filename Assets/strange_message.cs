using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strange_message : Item
{
    string[] parts;
    public override void useItem()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Investigate.16");
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("hint_checked"))
            {

                vending_manager.clue = true;
                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "TRUE" + "," + parts[3];
           
                break;
            }
         

        }
    }
}
