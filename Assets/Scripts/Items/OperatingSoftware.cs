using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatingSoftware : Item
{

    public override void useItem()
    {
        setDescriptionAfterUse("how you want to use it? you suppose to plug it into somewhere");
        Debug.Log("using operating software");
    }
}
