using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class security_station : InteractiveObj
{
    string[] parts;
    bool grant;
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);

        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("sec_station_checked") && parts[2].Equals("TRUE"))
            {



                grant = true;
                break;
            }
            else if (parts[0].Equals("sec_station_checked") && parts[2].Equals("FALSE")) {
                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "TRUE" + "," + parts[3];
                grant = false; break; }

        }

        if (grant) { player.talkToSelf("Response.security_station_2"); } else { player.talkToSelf("Response.security_station"); }

    }
}
