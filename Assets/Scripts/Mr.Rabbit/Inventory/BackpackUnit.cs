using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BackpackUnit : MonoBehaviour
{
    public Queue<GameObject> storedItem = new Queue<GameObject>();
    public Queue<Item> items = new Queue<Item>();
    private int index;
    [SerializeField] private Sprite icon;
    private string itemName, itemDescription, itemDescriptionAfterUse;
    [SerializeField] private Image itemImage;

    private PlayerControl playerControl;

    //getters & setters
    public int Index {get => index; set => index = value;}
    public Sprite Icon {get => icon; set => icon = value;}
    public string ItemName {get => itemName; set => itemName = value;}
    public string ItemDes {get => itemDescription; set => itemDescription = value;}
    public string ItemDescriptionAfterUse {get => itemDescriptionAfterUse; set => itemDescriptionAfterUse = value;}

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    
    void Update()
    {
        
    }


    /**
     * this function will be called in showInventory() function is called, from Inventory GUI
     * 
     * First : set its scale to right scale
     * Second : playing animation to create the unit
     * Third : set image 
     */
    public void showUnit()
    {
        //part 1


        //part 2

        //part 3
        if (Icon != null) {
            itemImage.color = new Color(255,255,255,255);
            itemImage.sprite = icon;
        }
        else
            itemImage.color = new Color(0,0,0,0);
    }

    public void setStoredItem(Item storedItem)
    {
        this.storedItem.Enqueue(storedItem.gameObject);
        itemName = storedItem.getName();
        itemDescription = storedItem.getDescription();
        itemDescriptionAfterUse = storedItem.getDescriptionAfterUse();
        icon = storedItem.getIcon();
    }

    public void setStoredItem(GameObject storedItem)
    {
        if (storedItem.GetComponent<Item>() != null)
            setStoredItem(storedItem.GetComponent<Item>());
        else
            Debug.LogWarning("trying to store a non-item object into backpack index " + index);
    }

    /**
     * reset the unit to null
     */
    public void resetStoredItem()
    {
        itemName = "";
        itemDescription = "";
        itemDescriptionAfterUse = "";
        icon = null;
    }

    /**
     * set current unit of inventory GUI to this one
     */
    public void chooseThisUnit()
    {
        InventoryGUI.currentItemIndex = index;
        InventoryGUIControl.currentUnit = this;
        FindObjectOfType<InventoryGUIControl>().CurrentIndex = Index;
    }

    public string getItemName()
    {
        return itemName;
    }

    public string getItemDesctription()
    {
        return itemDescription;
    }

    public string getItemDescriptionAfterUse()
    {
        return itemDescriptionAfterUse;
    }

    public Sprite getIcon()
    {
        return icon;
    }

    public void useItem()
    {
        GameObject currentItem;
        if (storedItem.Count > 0)
        {
            currentItem = storedItem.Peek();
        }
        else
            currentItem = null;
        
        if (currentItem != null)
        {
            Item item = currentItem.GetComponent<Item>();
            if (playerControl.InteractingObj == null) {
                item.useItem();
            } else {
                playerControl.InteractingObj.GetComponent<InteractiveObj>().useItem();
            }

            //if it's comsumable, remove it from the queue of the backpack unit as well as from playerbackpack list
            if (item.consumable)
            {
                storedItem.Dequeue();
                FindObjectOfType<PlayerBackpack>().backpack.Remove(currentItem);
            }

            if (storedItem.Count <= 0)
            {
                resetStoredItem();
            }
        }
    }
}
