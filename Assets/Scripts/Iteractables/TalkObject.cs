using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkObject : InteractiveObj
{
    private Talkable talk;

    void Start() {
        talk = GetComponent<Talkable>();    
    }

    public override void interact()
    {
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
        talk.getPlayer().NPCToTalk = gameObject;
        talk.getPlayer().talkToNPC();
    }
}
