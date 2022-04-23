using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class UIControl : MonoBehaviour
{
    [SerializeField, BoxGroup("Mr.Rabbit System GUI Container")] private GameObject inventoryContainer, neuroContainer, mapContainer, intelContainer;
    [SerializeField, BoxGroup("Selection Menu")] private GameObject selectionMenu, selectionIndicator, useButton, invenButton, neuroButton;
    [SerializeField, BoxGroup("Selection Menu"),ReorderableList] private List<Button> selectionButtons;

    [SerializeField, BoxGroup("Player")]private PlayerControl player;
    private KeyManager _key;
    [SerializeField, BoxGroup("General GUI")] private GameObject backgroundContainer, tabContainer;
    [SerializeField, BoxGroup("Inventory GUI")]private InventoryGUI inventoryGUI; //class that take care of present inventory
    [SerializeField, BoxGroup("Inventory GUI")] private InventoryGUIControl inventoryControl;
    [SerializeField, BoxGroup("Neuro Implant GUI")] private NeuroImplantGUI neuroGUI;
    [SerializeField, BoxGroup("Neuro Implant GUI")] private NeuroGUIControl neuroControl;
    [SerializeField, BoxGroup("Time GUI")] private GameObject timeGUI;
    [SerializeField, BoxGroup("Inventory GUI")] private Button inventoryTab;
    [SerializeField, BoxGroup("Neuro Implant GUI")] private Button neuroImplantTab;
    [SerializeField, BoxGroup("Map GUI")] private Button mapTab;
    [SerializeField, BoxGroup("Intel GUI")] private Button intelTab;
    private bool isInventoryOnly = false, isNeuroOnly = false, isInvestigateOnly = false;

    //delegate
    public delegate void CloseWindows();
    public CloseWindows closeWindows;

    public delegate void Open();
    public Open openGUI, closeGUI, disableTab, activeTab;

    //getters & setters
    public PlayerControl Player {get {return player;} private set {Debug.Log("setting player");player = value;}}
    public List<Button> SelectionButtons {get {return selectionButtons;} private set {selectionButtons = value;}}
    public GameObject SelectionIndicator {get {return selectionIndicator;} private set {selectionIndicator = value;}}
    public KeyManager Key {get {return _key;} private set {_key = value;}}
    public bool IsInventoryOnly {get => isInventoryOnly; set => isInventoryOnly = value;}
    public bool IsNeuroOnly {get => isNeuroOnly; set => isNeuroOnly = value;}
    public bool IsInvestigateOnly {get => isInventoryOnly; set => isInvestigateOnly = value;}

    //state machine
    public UIStateIdle stateIdle = new UIStateIdle();
    public UIStateSelection stateSelection = new UIStateSelection();
    public UIStateInventory stateInventory = new UIStateInventory();
    public UIStateNeuro stateNeuro = new UIStateNeuro();
    public UIStateInvestigate stateInvestigate = new UIStateInvestigate();
    public UIStateMap stateMap = new UIStateMap();
    public UIStateIntel stateIntel = new UIStateIntel();
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
    public void ChangeToInventoryOnlyState() {IsInventoryOnly = true; ChangeState(stateInventory);}
    public void ChangeToNeuroState() {ChangeState(stateNeuro);}
    public void ChangeToNeuroOnlyState() {IsNeuroOnly = true; ChangeState(stateNeuro);}
    public void ChangeToIdleState() {ChangeState(stateIdle);}
    public void ChangeToInvestigateState() {ChangeState(stateInvestigate);}
    public void ChangeToSelectionState() {ChangeState(stateSelection);}
    public void ChangeToMapState() {ChangeState(stateMap);}

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

        disableTab += disableInventoryTab;
        disableTab += disableIntelTab;
        disableTab += disableMapTab;
        disableTab += disableNeuroImplantTab;

        activeTab += activeintelTab;
        activeTab += activeInventoryTab;
        activeTab += activeNeuroImplantTab;
        activeTab += activeMapTab;
    }

    void Start()
    {
        Key = FindObjectOfType<KeyManager>();
        currentState = stateIdle;
    }

    
    void Update()
    {
        currentState.UpdateState(this);
    }

    #region close & open Windows

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
        inventoryTab.Select();
        ChangeState(stateInventory);
    }
    public void closeInventory() {
        inventoryContainer.SetActive(false);
    }

    public void openNeuro() {
        neuroContainer.SetActive(true);
        //neuroGUI.initializeAppArea();
        neuroImplantTab.Select();
        ChangeState(stateNeuro);
    }

    public void closeNeuro() {
        neuroContainer.SetActive(false);
    }

    public void openMap() {
        mapContainer.SetActive(true);
        mapTab.Select();
        ChangeState(stateMap);
    }

    public void closeMap() {
        mapContainer.SetActive(false);
    }

    public void openIntel() {
        intelContainer.SetActive(true);
        intelTab.Select();
        ChangeState(stateIntel);
    }

    public void closeIntel() {
        intelContainer.SetActive(false);
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

    public void disableInventoryTab() {
        inventoryTab.interactable = false;
    }

    public void activeInventoryTab() {
        inventoryTab.interactable = true;
    }

    public void disableNeuroImplantTab() {
        neuroImplantTab.interactable = false;
    }

    public void activeNeuroImplantTab() {
        neuroImplantTab.interactable = true;
    }

    public void disableMapTab() {
        mapTab.interactable = false;
    }

    public void activeMapTab() {
        mapTab.interactable = true;
    }

    public void disableIntelTab() {
        intelTab.interactable = false;
    }

    public void activeintelTab() {
        intelTab.interactable = true;
    }
    #endregion

    public void showItems() {
        inventoryControl.showItems();
    }

    public void showNeuroApps() {
        neuroControl.showApps();
    }

    /* use current selecting item of inventoryGUIControl */
    public void useItem() {
        inventoryControl.useItem();
        Debug.Log("used item in ui control");
    }

    /* use current selecting neuro implant of NeuroImplantGUIControl */
    public void useNeuroImplant() {
        neuroControl.useNeuroImplant();
    }
}
