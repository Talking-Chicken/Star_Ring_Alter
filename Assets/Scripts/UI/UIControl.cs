using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class UIControl : MonoBehaviour
{
    [SerializeField, BoxGroup("Mr.Rabbit System GUI Container")] private GameObject inventoryContainer, neuroContainer;
    [SerializeField, BoxGroup("Selection Menu")] private GameObject selectionMenu, selectionIndicator, useButton, invenButton, neuroButton;
    [SerializeField, BoxGroup("Selection Menu"),ReorderableList] private List<Button> selectionButtons;

    [SerializeField, BoxGroup("Player")]private PlayerControl player;
    private KeyManager _key;
    [SerializeField, BoxGroup("General GUI")] private GameObject backgroundContainer, tabContainer;
    [SerializeField, BoxGroup("Invetory GUI")]private InventoryGUI inventoryGUI; //class that take care of present inventory
    [SerializeField, BoxGroup("Neuro Impalnt GUI")] private NeuroImplantGUI neuroGUI;
    [SerializeField, BoxGroup("Time GUI")] private GameObject timeGUI;

    //delegate
    public delegate void CloseWindows();
    public CloseWindows closeWindows;

    public delegate void Open();
    public Open openGUI, closeGUI;

    //getters & setters
    public PlayerControl Player {get {return player;} private set {Debug.Log("setting player");player = value;}}
    public List<Button> SelectionButtons {get {return selectionButtons;} private set {selectionButtons = value;}}
    public GameObject SelectionIndicator {get {return selectionIndicator;} private set {selectionIndicator = value;}}
    public KeyManager Key {get {return _key;} private set {_key = value;}}

    //state machine
    public UIStateIdle stateIdle = new UIStateIdle();
    public UIStateSelection stateSelection = new UIStateSelection();
    public UIStateInventory stateInventory = new UIStateInventory();
    public UIStateNeuro stateNeuro = new UIStateNeuro();
    public UIStateInvestigate stateInvestigate = new UIStateInvestigate();
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
    public void ChangeToInvestigateState() {ChangeState(stateInvestigate);}
    public void ChangeToSelectionState() {ChangeState(stateSelection);}

    //change player state
    public void ChangePlayerState(PlayerStateBase newState) {Player.ChangeState(newState);}

    void Awake() {
        closeWindows += closeSelectionMenu;
        closeWindows += closeInventory;
        closeWindows += closeNeuro;

        openGUI += openBackground;
        openGUI += openTabs;

        closeGUI += closeBackground;
        closeGUI += closeTabs;
    }

    void Start()
    {
        Key = FindObjectOfType<KeyManager>();
        currentState = stateIdle;
    }

    
    void Update()
    {
        currentState.UpdateState(this);
        Debug.Log(currentState);
    }

    #region close & open Windows
    //open and close tabs

    /**
    * create (set active) selection menu object (inventory & neruo implant selection) as default 
    * create other menu if applicable
    * position of selection menu should be above player's interactingObj  
    */
    public void openSelectionMenu() {
        if (Player == null) Debug.Log("player is null");

        if (Player.DetectingObj != null && Player.DetectingObj != Player.gameObject) {
            FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateUI);
            ChangeState(stateSelection);
            selectionMenu.SetActive(true);
            InteractiveObj interactObj = player.DetectingObj.GetComponent<InteractiveObj>();
            if (interactObj.CanUse) useButton.SetActive(true); else useButton.SetActive(false);
            if (interactObj.CanInven) invenButton.SetActive(true); else invenButton.SetActive(false);
            if (interactObj.CanNeuro) neuroButton.SetActive(true); else neuroButton.SetActive(false);

            selectionMenu.transform.position = Camera.main.WorldToScreenPoint(player.DetectingObj.transform.position);
        }
    }

    public void closeSelectionMenu() {
        selectionMenu.SetActive(false);
    }

    
    public void openInventory() {
        inventoryContainer.SetActive(true);
        //inventoryGUI.showInventory();
        ChangeState(stateInventory);
    }
    public void closeInventory() {
        inventoryContainer.SetActive(false);
    }

    public void openNeuro() {
        neuroContainer.SetActive(true);
        //neuroGUI.initializeAppArea();
        ChangeState(stateNeuro);
    }

    public void closeNeuro() {
        neuroContainer.SetActive(false);
    }

    public void openTime() {
        timeGUI.gameObject.SetActive(true);
        Time_text.isTimePaused = false;
    }

    public void closeTime() {
        timeGUI.gameObject.SetActive(false);
        Time_text.isTimePaused = true;
    }

    public void openBackground() {
        backgroundContainer.SetActive(true);
    }

    public void closeBackground() {
        backgroundContainer.SetActive(false);
    }

    public void openTabs() {
        tabContainer.SetActive(true);
    }

    public void closeTabs() {
        tabContainer.SetActive(false);
    }
    #endregion
}
