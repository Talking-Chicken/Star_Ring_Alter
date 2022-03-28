using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using NaughtyAttributes;

/**
 * control player direction as well as NPC to talk with
 */
public class PlayerControl : MonoBehaviour
{   
    public bool CanMove {get; set;}


    [BoxGroup("Dialogue")] public GameObject NPCToTalk;
    public static bool canTalk = false;
    [SerializeField, BoxGroup("Dialogue")] public DialogueRunner dialogueRunner;
    [SerializeField, BoxGroup("Dialogue")] private DialogueUI dialogueUI;

    public static bool show_invest;

    //components
    Rigidbody2D myBody;
    private PlayerBackpack playerBackpack;
    private KeyManager key;
    [SerializeField, BoxGroup("UI")] private UIControl uiControl;

    //detect and interact
    [SerializeField, Range(5.0f, 15.0f)] private float _sight; 
    [ShowNonSerializedField] private GameObject detectingObj = null;

    //getters & setters
    public float Sight{get {return _sight;} private set {_sight = value;}}
    public GameObject DetectingObj {get {return detectingObj;} set {detectingObj = value;}}
    public KeyManager KeyManager {get {return key;} private set {key = value;}}
    public UIControl UIControl {get {return uiControl;} private set {uiControl = value;}}
    

    //state machine
    private PlayerStateBase currentState, previousState;
    public PlayerStateExplore stateExplore = new PlayerStateExplore();
    public PlayerStateDialogue stateDialogue = new PlayerStateDialogue();
    public PlayerStateUI stateUI = new PlayerStateUI();

    public void ChangeToPreviousState() {StartCoroutine(waitToChange());} //change to previous state 0.5 seconds delay
    IEnumerator waitToChange() {yield return new WaitForSeconds(0.5f); ChangeState(previousState);} //where delay actually happens

    public void ChangeState(PlayerStateBase newState)
    {
        if (currentState != null)
        {
            currentState.LeaveState(this);
        }

        previousState = currentState;
        currentState = newState;

        if (currentState != null)
        {  
            currentState.EnterState(this);
        }
    }

    void Start()
    {
        ChangeState(stateExplore);
        myBody = GetComponent<Rigidbody2D>();
        key = FindObjectOfType<KeyManager>();
        playerBackpack = GetComponent<PlayerBackpack>();
    }

    void Update()
    {
        currentState.UpdateState(this);
        
       
    }

    private void FixedUpdate()
    {
        currentState.FixedupdateState(this);
    }
  
    /**
     * talk to NPC that is currently selecting and has a Talkable component (either in children or in main body)
     */
    public void talkToNPC()
    {
        ChangeState(stateDialogue);
        Talkable NPC = null;
        if (NPCToTalk.GetComponentInChildren<Talkable>() != null)
            NPC = NPCToTalk.GetComponentInChildren<Talkable>();
        else if (NPCToTalk.GetComponent<Talkable>() != null)
            NPC = NPCToTalk.GetComponent<Talkable>();
        else
            Debug.LogWarning("cannot find talkable script for " + NPCToTalk.name);

        //add Yarn Program of NPC to dialogue runner, then start the dialogue
        if (NPC != null) {
            dialogueRunner.startNode = NPC.getStartNode();
            if (!dialogueRunner.NodeExists(dialogueRunner.startNode))
                dialogueRunner.Add(NPC.getDialogueFile());
            
            dialogueRunner.StartDialogue();
        }
    }

    /**
     * talk to NPC that is currently selecting and has a talkable component, with this specity start node
     * @param startNode string that dialogue runner is starting
     */
    public void talkToNPC(string startNode)
    {
        ChangeState(stateDialogue);
        Talkable NPC = null;
        if (NPCToTalk.GetComponentInChildren<Talkable>() != null)
            NPC = NPCToTalk.GetComponentInChildren<Talkable>();
        else if (NPCToTalk.GetComponent<Talkable>() != null)
            NPC = NPCToTalk.GetComponent<Talkable>();
        else
            Debug.LogWarning("cannot find talkable script for " + NPCToTalk.name);

        //add Yarn Program of NPC to dialogue runner, then start the dialogue
        if (NPC != null)
        {
            dialogueRunner.startNode = startNode;
            if (!dialogueRunner.NodeExists(dialogueRunner.startNode))
                dialogueRunner.Add(NPC.getDialogueFile());

            dialogueRunner.StartDialogue();
        }
    }

    /**
     * raycast to detect a gameobject that contains interactable component
     * @return true if detected one, false other wise
     */
    public bool detectInteractiveObj() {
        RaycastHit2D hit;
        if (GetComponent<IsometricPlayerMovementController>() != null)
            hit = Physics2D.Raycast(gameObject.transform.position, GetComponent<IsometricPlayerMovementController>().dir, Sight);
        else {
            hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, Sight);
            Debug.LogWarning("doesn't detect isometric player movement controller");
        }
    
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponentInParent<Item>() != null)
            {
                detectingObj = hit.collider.gameObject.GetComponentInParent<Item>().gameObject;
                return true;
            }
            else if (hit.collider.gameObject.GetComponent<InteractiveObj>() != null)
            {
                detectingObj = hit.collider.gameObject.GetComponent<InteractiveObj>().gameObject;
                return true;
            } 
            else if (hit.collider.gameObject.GetComponent<Talkable>() != null) 
            {
                NPCToTalk = hit.collider.gameObject;
                return true;
            } 
            else 
            {
                detectingObj = gameObject;
                NPCToTalk = gameObject;
                return false;
            }
        }
        else
        {
            detectingObj = gameObject;
            NPCToTalk = gameObject;
            return false;
        }
    }

     /**
     * interact with the interactive object, and change to ui state
     * else pickup this object
     */
    public void interact(GameObject interactingObject) {
        if (interactingObject.GetComponent<InteractiveObj>() != null)
        {
            //interactingObject.GetComponent<InteractiveObj>().interact();
            UIControl.openSelectionMenu();
            ChangeState(stateUI);
        } else
        {
            pickUp();
        }
    }

    /**
     * add detectingObj to player's backpack
     * then set detectingObj to null
     */
    public void pickUp()
    {
        playerBackpack.add(detectingObj);
       
        detectingObj.SetActive(false);
        detectingObj = null;
    }
}
