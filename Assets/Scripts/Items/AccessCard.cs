using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCard : Item
{
    public int level = 1;

    public AccessCard()
    {
        base.setName("access card");
        base.setDescription("The personal work card of a formal employee of a convenience store. In order to prevent accidental access to the working areas of the shop, the convenience store is divided into different regions based on permission levels. Only those with the appropriate level of access can enter the designated areas.According to Mr.Rabbit, this specific card belongs to employee Lacey.");
        base.setDescriptionAfterUse("where should I insert the card?");
    }

    public override void useItem()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response_player_action.access_card");
    }
}
