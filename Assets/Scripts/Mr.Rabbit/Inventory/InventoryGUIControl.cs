using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;
using System.Text.RegularExpressions;

/* this class control the look and operation of inventory section of Mr.Rabbit*/
public class InventoryGUIControl : MonoBehaviour
{
    [SerializeField, BoxGroup("Description Section")] 
    private TextMeshProUGUI itemTypeText, itemNameText, itemDesText;
    [SerializeField, BoxGroup("Backpack Units")]
    private List<BackpackUnit> bakcpackUnits;

    [SerializeField, BoxGroup("Backpack Units")] private GridLayoutGroup grid;

    //index of currently selected backpack unit from List bakcpackUnits
    private int currentIndex;

    public static BackpackUnit currentUnit;

    [SerializeField] private PlayerControl player;
    [SerializeField] private PlayerBackpack playerBackpack;

    private List<Item> items = new List<Item>();

    //getters & setters
    public int CurrentIndex {get => currentIndex; set => currentIndex = value;}
    void Start()
    {
        // player = FindObjectOfType<PlayerControl>();
        // playerBackpack = FindObjectOfType<PlayerBackpack>();
        //check if everything is set up
        if (itemTypeText == null) Debug.LogWarning("item type text is null");
        if (itemNameText == null) Debug.LogWarning("item name text is null");
        if (itemDesText == null) Debug.LogWarning("item description text is null");
        if (bakcpackUnits == null) Debug.LogWarning("backpack units is null");
        if (grid == null) Debug.LogWarning("grid layout group is null");

        for (int i = 0; i < bakcpackUnits.Count; i++) {
            bakcpackUnits[i].Index = i;
        }

        setCurrentUnit(0);
    }

    
    void Update()
    {
        CurrentIndex = currentIndex;
        changeCurrentUnit();
    }

    /* set imgae, description, name, and type of the item to backpackUnit
       if there's no item sign to backpackUnit, then no icon will be shown*/
    public void showItems() {
        //link item list to player backpack
        items.Clear();
        for (int i = 0; i < playerBackpack.backpack.Count; i++) {
            if (playerBackpack.backpack[i].GetComponent<Item>() != null) {
                items.Add(playerBackpack.backpack[i].GetComponent<Item>());
            } else
                Debug.LogWarning(playerBackpack.backpack[i].name + " doesn't have a item component");
        }
        
        //set infomation of item to backpackUnit and show them
        for (int i = 0; i < bakcpackUnits.Count; i++)
        {
            bakcpackUnits[i].items.Clear();

            // foreach(Item item in items) {
            //     if (bakcpackUnits[i].items.Count > 0) {
            //         if (item.ItemName.Equals(bakcpackUnits[i].items.Peek().ItemName)) {
            //             bakcpackUnits[i].items.Enqueue(item);
            //             items.Remove(item);
            //         }
            //     } else {
            //         bakcpackUnits[i].items.Enqueue(item);
            //     }
            // }
            if (items.Count > i)
                bakcpackUnits[i].items.Enqueue(items[i]);

            if (bakcpackUnits[i].items.Count > 0) {
                bakcpackUnits[i].Icon = items[i].getIcon();
                bakcpackUnits[i].ItemName = items[i].getName();
                bakcpackUnits[i].ItemDes = items[i].getDescription();
                bakcpackUnits[i].ItemDes = items[i].getDescriptionAfterUse();
            } else {
                bakcpackUnits[i].resetStoredItem();
            }
            bakcpackUnits[i].showUnit();
        }

        //set current Unit back to the first one
        setCurrentUnit(0);
    }

    /* show name, type, and description of item in GUI*/
    public void showInfo(Item item) {
        if (item != null) {
            //make spaces between lower and upper case letters 
            itemTypeText.text = Regex.Replace(item.Type.ToString(), @"([a-z])([A-Z])", "$1 $2");
            itemDesText.text = item.getDescription();
            itemNameText.text = item.getName();
        } else {
            itemTypeText.text = "Type";
            itemDesText.text = "Description";
            itemNameText.text = "Name";
        }
    }

    public void changeCurrentUnit() {
        int targetIndex = currentIndex;
        if (Input.GetKeyDown(KeyCode.A))
            targetIndex = Mathf.Max(0,currentIndex-1);
        if (Input.GetKeyDown(KeyCode.D))
            targetIndex = Mathf.Min(currentIndex+1, bakcpackUnits.Count-1);
        if (Input.GetKeyDown(KeyCode.W))
            targetIndex = Mathf.Max(0, currentIndex - grid.constraintCount);
        if (Input.GetKeyDown(KeyCode.S))
            targetIndex = Mathf.Min(currentIndex + grid.constraintCount, bakcpackUnits.Count-1);

        if (targetIndex != currentIndex) { 
            if (checkValid(targetIndex)) {
                setCurrentUnit(targetIndex);
            } else {
                Debug.LogWarning(targetIndex + " is out of bound of backpackUnits list");
            }
        }
    }

    /* return true if targetIndex is a valid index of the backpackUnits list, false otherwise
       @param targetIndex the index number that player is trying to reach in the backpakcUnits list*/
    public bool checkValid(int targetIndex) {
        if (targetIndex < bakcpackUnits.Count && targetIndex >= 0)
            return true;
        return false;
    }

    /* set current unit to bakcpackUnits[index] by invoke the button of that backpackUnit
       @param index index of the backpackUnits that should be set to current unit*/
    public void setCurrentUnit(int index) {
        bakcpackUnits[index].GetComponent<Button>().Select();
        bakcpackUnits[index].GetComponent<Button>().onClick.Invoke();

        if (currentUnit.items.Count > 0)
            showInfo(currentUnit.items.Peek());
        else
            showInfo(null);
    }
    
    /* use current unit's item*/
    public void useItem() {
        currentUnit.useItem();
        Debug.Log("used item in inventory gui control ");
    }

}
