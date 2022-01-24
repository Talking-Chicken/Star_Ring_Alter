using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName, itemDescription, itemDescriptionAfterUse;
    [SerializeField] private Sprite icon;
    [SerializeField] private Color themeColor;

    //to check if we want to destroy this item when using it
    public bool consumable;

    public InteractiveObj interactiveObj { get; private set; }
    void Start()
    {
        //connecting all things
        if (gameObject.GetComponentInChildren<InteractiveObj>() != null)
        {
            interactiveObj = gameObject.GetComponentInChildren<InteractiveObj>();
        }

    }
    

    public string getName()
    {
        return itemName;
    }

    public void setName(string itemName)
    {
        this.itemName = itemName;
    }

    public Sprite getIcon()
    {
        return icon;
    }

    public void setIcon(Sprite icon)
    {
        this.icon = icon;
    }

    public string getDescription()
    {
        return itemDescription;
    }

    public void setDescription(string description)
    {
        itemDescription = description;
    }

    public string getDescriptionAfterUse()
    {
        return itemDescriptionAfterUse;
    }

    public void setDescriptionAfterUse(string description)
    {
        itemDescriptionAfterUse = description;
    }

    public void pickingUp()
    {
        gameObject.SetActive(false);
    }

    public virtual void useItem()
    {
        Destroy(gameObject);
    }
}
