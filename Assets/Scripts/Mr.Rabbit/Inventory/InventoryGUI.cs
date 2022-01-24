using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryGUI : MonoBehaviour
{
    public static int currentItemIndex = 0;

    [Header("Unit"), SerializeField] GameObject Unit;
    [Header("Player Backpack"), SerializeField] BackPack playerBackpack;

    [Header("Allignment")]
    [SerializeField] int row;
    [SerializeField] int collumn;
    [SerializeField] float xGap, yGap, xStart, yStart;
    private float XXGap, YYGap;

    [Header("Component")]
    [SerializeField] private GameObject backpackBG;
    [SerializeField] private GameObject indicator;
    private RectTransform indicatorTransform;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image image;

    //helper variables
    int currentSize;
    public static BackpackUnit currentUnit;
    private bool itemUsed = false;

    //store all units
    private BackpackUnit[] units;

    //common outside managers
    KeyManager key;

    void Start()
    {
        units = new BackpackUnit[collumn * row];

        //set up player backpack
        playerBackpack = FindObjectOfType<PlayerBackpack>();

        //set up units according to row and collumn
        currentSize = 0;
        for (int i = 0; i < collumn; i++)
        {
            for (int n = 0; n < row; n++)
            {
                if (Unit != null)
                {
                    GameObject g = Instantiate(Unit, new Vector2(xStart + (n * xGap), yStart - (i * yGap)), Unit.transform.rotation);
                    g.GetComponent<RectTransform>().SetParent(backpackBG.GetComponent<RectTransform>());
                    units[currentSize] = g.GetComponent<BackpackUnit>();
                    units[currentSize].setIndex(currentSize);
  
                    currentSize++;
                } else
                {
                    Debug.LogWarning("no Backpack Unit object");
                }
            }
        }

        //set current unit to default : 0
        currentUnit = units[0];

        //set up indicator
        if (indicator.GetComponent<RectTransform>() != null)
            indicatorTransform = indicator.GetComponent<RectTransform>();
        else
            Debug.LogWarning("this indicator is not a UI object yet");

        //set up key manager
        key = FindObjectOfType<KeyManager>();
        
    }

    
    void Update()
    {
        currentUnit = units[currentItemIndex];
        indicatorTransform.position = currentUnit.GetComponent<RectTransform>().position;
        
        //Keyboard Control
        if (Input.GetKeyDown(key.next) && currentItemIndex < (collumn * row - 1))
        {
            currentItemIndex++;
            itemUsed = false;
        }
        if (Input.GetKeyDown(key.previous) && currentItemIndex > 0)
        {
            currentItemIndex--;
            itemUsed = false;
        }

        //Mouse Control (more priority)


        showDetail();
    }

    /**
     * this function will be called when player backpack opens
     * 
     * First : store items in each unit, make sure there's no empty unity between two contiguous units
     * Second : show animation of them appearing. Meanwhile, audio will sound
     */
    public void showInventory()
    {
        //first part
        //make sure current index is starting from zero
        currentItemIndex = 0;
        for (int i = 0; i < playerBackpack.backpack.Count; i++)
        {
            if (playerBackpack.backpack.Count > 0)
            {
                GameObject item = playerBackpack.backpack[i];
                units[i].setStoredItem(item);
            }
        }

        //also set all items after the playerbackpack size to be null
        for (int i = playerBackpack.backpack.Count; i < currentSize; i++)
        {
            units[i].resetStoredItem();
        }

        //second part

        foreach (BackpackUnit unit in units)
        {
            unit.showUnit();
        }
    }

    public void showDetail()
    {
        nameText.text = currentUnit.getItemName();

        if (!itemUsed)
            descriptionText.text = currentUnit.getItemDesctription();
        else
            descriptionText.text = currentUnit.getItemDescriptionAfterUse();

        image.sprite = currentUnit.getIcon();
    }

    public void useItem()
    {
        currentUnit.useItem();
        itemUsed = true;
        

        //有问题
        /*
        if (playerBackpack.backpack.Count + 1 > currentItemIndex)
            playerBackpack.backpack.RemoveAt(currentItemIndex);
        */
    }
}
