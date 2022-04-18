using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ItemType {Comsumable, ImportantItem, AccessPermission}

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName, itemDescription, itemDescriptionAfterUse;
    [SerializeField] private Sprite icon;
    [SerializeField] private Color themeColor;
    [SerializeField] private ItemType type;

    //to check if we want to destroy this item when using it
    public bool consumable;

    public InteractiveObj interactiveObj { get; private set; }

    //getters & setters
    public string ItemName {get {return itemName;} set {itemName = value;}}
    public ItemType Type {get => type; set => type = value;}
    public string ItemDes {get => itemDescription; set => itemDescription = value;}
    public string ItemDescriptionAfterUse {get => itemDescriptionAfterUse; set => itemDescriptionAfterUse = value;}
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

    public virtual void useItemToInteractiveObj() {
        Destroy(gameObject);
    }

    public virtual int compareTo(Item other) {
        return ItemName.CompareTo(other.ItemName);
    }
}
