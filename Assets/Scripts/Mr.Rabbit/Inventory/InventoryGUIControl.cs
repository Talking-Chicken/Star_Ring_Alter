using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;

/* this class control the look and operation of inventory section of Mr.Rabbit*/
public class InventoryGUIControl : MonoBehaviour
{
    [SerializeField, BoxGroup("Description Section")] 
    private TextMeshProUGUI itemTypeText, itemNameText, itemDesText;
    [SerializeField, BoxGroup("Backpack Units")]
    private List<BackpackUnit> bakcpackUnits;

    public static BackpackUnit currentUnit;
    void Start()
    {
        //check if everything is set up
        if (itemTypeText == null) Debug.LogWarning("item type text is null");
        if (itemNameText == null) Debug.LogWarning("item name text is null");
        if (itemDesText == null) Debug.LogWarning("item description text is null");
        if (bakcpackUnits == null) Debug.LogWarning("backpack units is null");
    }

    
    void Update()
    {
        
    }

    public void showInventory() {
        
    }

    public void setCurrentUnit() {
            
    }

    
}
