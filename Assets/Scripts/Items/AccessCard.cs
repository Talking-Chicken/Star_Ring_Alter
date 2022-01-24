using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCard : Item
{
    public int level = 1;

    public AccessCard()
    {
        base.setName("access card");
        base.setDescription("the card that grant you room access");
        base.setDescriptionAfterUse("where should I insert the card?");
    }

    public override void useItem()
    {
        
    }
}
