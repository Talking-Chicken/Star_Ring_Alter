using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : Item
{
    [SerializeField]
    string description, descriptionAfterUse;

    void Start()
    {
            
    }

    
    void Update()
    {
        
    }

    //@override
    public override void useItem()
    {
        //descriptionText.text = descriptionAfterUse;
    }
}
