using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrRabbitTalk : Talkable
{

    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null)
        {
            getPlayer().NPCToTalk = gameObject;
            getPlayer().talkToNPC();
            Destroy(this);
        }
    }
}
