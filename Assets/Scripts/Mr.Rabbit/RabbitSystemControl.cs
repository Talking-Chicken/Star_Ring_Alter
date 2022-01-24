using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public enum UIState
{
    Empty, Status, Inventory, Quest, Codex, Map, Neuro, Test
}

public class RabbitSystemControl : MonoBehaviour
{
    StateManager state;
    [SerializeField, BoxGroup("Functions")] GameObject background, status, inventory, quest, codex, map, neuro;

    public static UIState currentUIState = UIState.Empty;
        
    private StateManager stateManager;
    private KeyManager key;

    [Header("Debug - optional"), SerializeField]
    private Text testText;
    
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        key = FindObjectOfType<KeyManager>();
    }

    
    void Update()
    {
        //switchUISection();
       // Debug.Log(camera_pos_setter.rabbit_on);
       if (stateManager.getCurrentState() == State.Explore)
        {
            enterDefaultUI();

            if (Input.GetKeyDown(key.openNeuro))
                transitionUIState(UIState.Neuro);

        } else if (stateManager.getCurrentState() == State.UI)
        {
            if (Input.GetKeyDown(key.openRabbit))
                exitUI();
            if (Input.GetKeyDown(key.openNeuro))
                transitionUIState(UIState.Neuro);
        }

        //test for UIstate
        if (testText != null)
            testText.text = getCurrentUIState().ToString();

        if (Input.GetKeyDown(KeyCode.P))
            exitUI();
    }

    /**
     * transit to a new UI state
     * only transit to new state when not in new state. else, return to rabbit state.
     */
    public void transitionUIState(UIState newState)
    {
        switch (newState)
        {
            case UIState.Status:
                if (currentUIState == newState && state.getCurrentState() != State.UI)
                {
                    onCloseStatus();
                    transitionUIState(UIState.Empty);
                    break;
                }
                else
                {
                    stateManager.transitionState(State.UI);
                    onOpenBackground();
                    onCloseInventory();
                    onCloseQuest();
                    onCloseCodex();
                    onCloseNeuro();
                    onOpenStatus();
                    currentUIState = newState;
                    break;
                }
            case UIState.Quest:
                if (currentUIState == newState)
                {
                    onCloseQuest();
                    transitionUIState(UIState.Empty);
                    break;
                }
                else
                {
                    stateManager.transitionState(State.UI);
                    onCloseCodex();
                    onCloseNeuro();
                    onOpenQuest();
                    currentUIState = newState;
                    break;
                }
            case UIState.Inventory:
                if (currentUIState == newState)
                {
                    exitUI();
                    break;
                }
                else
                {
                    stateManager.transitionState(State.UI);
                    onOpenBackground();
                    onCloseQuest();
                    onCloseMap();
                    onCloseCodex();
                    onCloseStatus();
                    onCloseNeuro();
                    onOpenInventory();
                    currentUIState = newState;
                    break;
                }
            case UIState.Codex:
                if (currentUIState == newState)
                {
                    onCloseCodex();
                    transitionUIState(UIState.Empty);
                    break;
                }
                else
                {
                    stateManager.transitionState(State.UI);
                    onCloseInventory();
                    onCloseQuest();
                    onCloseNeuro();
                    onOpenCodex();
                    currentUIState = newState;
                    break;
                }
            case UIState.Map:
                if (currentUIState == newState)
                {
                    onCloseMap();
                    transitionUIState(UIState.Empty);
                    currentUIState = newState;
                    break;
                } else
                {
                    stateManager.transitionState(State.UI);
                    onCloseCodex();
                    onCloseInventory();
                    onCloseQuest();
                    onCloseNeuro();
                    onOpenMap();
                    currentUIState = newState;
                    break;
                }
            case UIState.Neuro:
                if (currentUIState == newState)
                {
                    exitUI();
                    break;
                } else
                {
                    stateManager.transitionState(State.UI);
                    onCloseCodex();
                    onCloseInventory();
                    onCloseMap();
                    onCloseQuest();
                    onCloseStatus();
                    onOpenNeuro();
                    currentUIState = newState;
                    break;
                }
            case UIState.Test:
                if (currentUIState == newState)
                {
                    exitUI();
                    Debug.Log("closing test UIstate");
                    break;
                } else
                {
                    stateManager.transitionState(State.UI);
                    onCloseCodex();
                    onCloseInventory();
                    onCloseMap();
                    onCloseQuest();
                    onCloseStatus();
                    onCloseNeuro();
                    currentUIState = newState;
                    Debug.Log("entering testing state");
                    break;
                }
            case UIState.Empty:
                onCloseInventory();
                onCloseQuest();
                onCloseCodex();
                onCloseStatus();
                onCloseBackground();
                onCloseNeuro();
                currentUIState = newState;
                break;
        }
    }

    void onOpenStatus()
    {
        status.SetActive(true);
    }

    void onOpenInventory()
    {
        inventory.SetActive(true);
        inventory.GetComponentInChildren<InventoryGUI>().showInventory();
    }

    void onOpenQuest()
    {
        quest.SetActive(true);
    }

    void onOpenCodex()
    {
        
        codex.SetActive(true);
    }

    void onCloseStatus()
    {
        status.SetActive(false);
    }

    void onCloseInventory()
    {
        inventory.SetActive(false);
    }

    void onCloseQuest()
    {
        quest.SetActive(false);
    }

    void onCloseCodex()
    {
        codex.SetActive(false);
    }

    void onOpenMap()
    {
        map.SetActive(true);
    }

    void onCloseMap()
    {
        map.SetActive(false);
    }

    public void onOpenBackground()
    {
        background.SetActive(true);
    }

    public void onCloseBackground()
    {
        background.SetActive(false);
    }

    public void onOpenNeuro()
    {
        neuro.SetActive(true);
    }

    public void onCloseNeuro()
    {
        neuro.SetActive(false);
    }

    public UIState getCurrentUIState()
    {
        return currentUIState;
    }

    /**
     * entering default (status) UI
     * then, set state to UI state;
     */
    private void enterDefaultUI()
    {
        if (Input.GetKeyDown(key.openRabbit) || Input.GetKeyDown(key.openStatus))
            transitionUIState(UIState.Status);
    }

    /**
     * exit UI state (transit to UI State empty)
     * then, set state to Explore state;
     */
    public void exitUI()
    {
        transitionUIState(UIState.Empty);
        stateManager.transitionState(State.Explore);
        Debug.Log("exiting UI");
    }

}
