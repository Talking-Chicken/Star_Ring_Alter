using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vending_machine : InteractiveObj
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response_player_action.Rita.1");
    }
}
