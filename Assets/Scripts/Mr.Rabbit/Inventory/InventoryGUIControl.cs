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

    [SerializeField, BoxGroup("Backpack Units")] private GridLayoutGroup grid;

    //index of currently selected backpack unit from List bakcpackUnits
    private int currentIndex;

    public static BackpackUnit currentUnit;

    private PlayerControl player;

    //getters & setters
    public int CurrentIndex {get => currentIndex; set => currentIndex = value;}
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
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
        Debug.Log(currentUnit.name);
    }

    public void showItem() {
        
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
        currentIndex = index;
    }
    
}
