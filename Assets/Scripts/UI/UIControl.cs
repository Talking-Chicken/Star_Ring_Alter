using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class UIControl : MonoBehaviour
{
    [SerializeField, BoxGroup("Mr.Rabbit System GUI Container")] private GameObject inventoryContainer, neuroContainer;
    [SerializeField, BoxGroup("Selection Menu")] private GameObject selectionMenu, selectionIndicator;
    [SerializeField, BoxGroup("Selection Menu")] private List<Button> selectionButtons;

    private PlayerControl player;
    private KeyManager _key;
    [SerializeField, BoxGroup("Invetory GUI")]private InventoryGUI inventoryGUI; //class that take care of present inventory

    //getters & setters
    public PlayerControl Player {get {return player;} private set {player = value;}}
    public List<Button> SelectionButtons {get {return selectionButtons;} private set {selectionButtons = value;}}
    public GameObject SelectionIndicator {get {return selectionIndicator;} private set {selectionIndicator = value;}}
    public KeyManager Key {get {return _key;} private set {_key = value;}}

    //state machine
    public UIStateIdle stateIdle = new UIStateIdle();
    public UIStateSelection stateSelection = new UIStateSelection();
    public UIStateInventory stateInventory = new UIStateInventory();
    public UIStateNeuro stateNeuro = new UIStateNeuro();
    private UIStateBase currentState;

    public void ChangeState(UIStateBase newState)
    {
        if (newState != currentState) {
            if (currentState != null)
            {
                currentState.LeaveState(this);
            }

            currentState = newState;

            if (currentState != null)
            {  
                currentState.EnterState(this);
            }
        }
    }

    //those change to state are used for unity button on click events
    public void ChangeToInventoryState() {ChangeState(stateInventory);}
    public void ChangeToNeuroState() {ChangeState(stateNeuro);}
    public void ChangeToIdleState() {ChangeState(stateIdle);}

    //change player state
    public void ChangePlayerState(PlayerStateBase newState) {player.ChangeState(newState);}

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        Key = FindObjectOfType<KeyManager>();
        currentState = stateIdle;
    }

    
    void Update()
    {
        currentState.UpdateState(this);
        Debug.Log(currentState);
    }

    /**
    * create (set active) selection menu object (inventory & neruo implant selection) as default 
    * create other menu if applicable
    * position of selection menu should be above player's interactingObj  
    */
    public void openSelectionMenu() {
        if (player.DetectingObj != null && player.DetectingObj != player.gameObject) {
            FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateUI);
            ChangeState(stateSelection);
            selectionMenu.SetActive(true);
            selectionMenu.transform.position = Camera.main.WorldToScreenPoint(player.DetectingObj.transform.position);
        }
    }

    /**
    * set collection menu object inactive 
    */
    public void closeSelectionMenu() {
        selectionMenu.SetActive(false);
    }

    /**
    * open inventory
    */
    public void openInventory() {
        inventoryContainer.SetActive(true);
        inventoryGUI.showInventory();
        ChangeState(stateInventory);
    }

    /**
    * close inventory
    */
    public void closeInventory() {
        inventoryContainer.SetActive(false);
    }
}
