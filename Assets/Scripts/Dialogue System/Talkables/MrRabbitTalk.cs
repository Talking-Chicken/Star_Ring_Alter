using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrRabbitTalk : Talkable
{
    [Tooltip("enable it means this area will not be talkable anymore when triggered a conversation once")]
    public bool doOnce = true;

    private bool inCoolDown = false;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null && !inCoolDown)
        {
            
            getPlayer().NPCToTalk = gameObject;
            getPlayer().talkToNPC();

            if (doOnce)
                Destroy(this);

            inCoolDown = true;
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null && !inCoolDown)
        {
            getPlayer().NPCToTalk = gameObject;
            getPlayer().talkToNPC();

            if (doOnce)
                Destroy(this);

            inCoolDown = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        inCoolDown = false;
    }
}
