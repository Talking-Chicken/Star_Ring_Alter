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
    [SerializeField] private bool isUsingThisToMove;
    [SerializeField, Header("Movement"), Range(0f, 1000f), ShowIf("isUsingThisToMove")] float speed;
    [ShowIf("isUsingThisToMove")]public bool canMove;

    public Vector2 Dir { get; private set; } //direction that player is facing [cannot be (0,0)]
    private Vector2 movement = new Vector2(0,0); //direction that player is going [can be (0,0)]

    [Header("Dialogue")] public GameObject NPCToTalk;
    public static bool canTalk = false;
    [SerializeField] public DialogueRunner dialogueRunner;
    [SerializeField] private DialogueUI dialogueUI;

    //detect and interact
    [SerializeField, Range(5.0f, 15.0f)] private float _sight; 
    private GameObject detectingObj = null;

    //getters & setters
    public float Sight{get {return _sight;} private set {_sight = value;}}
    public GameObject DetectingObj {get {return detectingObj;} set {detectingObj = value;}}

    Rigidbody2D myBody;
    private PlayerBackpack playerBackpack;
    private KeyManager key;

    //setters & getters
    public KeyManager KeyManager {get {return key;} private set {key = value;}}

    //state machine
    private PlayerStateBase currentState;
    public PlayerStateExplore stateExplore = new PlayerStateExplore();
    public PlayerStateDialogue stateDialogue = new PlayerStateDialogue();
    public PlayerStateUI stateUI = new PlayerStateUI();

    public void ChangeState(PlayerStateBase newState)
    {
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

    void Start()
    {
        currentState = stateExplore;
        myBody = GetComponent<Rigidbody2D>();
        key = FindObjectOfType<KeyManager>();
        playerBackpack = GetComponent<PlayerBackpack>();
    }

    void Update()
    {
        setDir();
        /*if (canTalk)
        {
            if (Input.GetKeyDown(key.talk) && !dialogueRunner.IsDialogueRunning)
                talkToNPC();
        }

        if (detectInteractiveObj() && Input.GetKeyDown(key.interact)) {
            interact(detectingObj);
        }*/
        currentState.UpdateState(this);
        
    }

    private void FixedUpdate()
    {
        if (isUsingThisToMove)
        {
            if (canMove)
            {
                handleMovement();
            }
            else
            {
                stopMovement();
            }
        }
    }

    void setDir() //give dir a Vector2 value, according to WASD that player is pressing
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
            {
                Dir = new Vector2(-0.75f, 0.75f);
                movement = Dir;
            } else if (Input.GetKey(KeyCode.D))
            {
                Dir = new Vector2(0.75f, 0.75f);
                movement = Dir;
            }  else
            {
                Dir = new Vector2(0, 1);
                movement = Dir;
            }
        } else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                Dir = new Vector2(-0.75f, -0.75f);
                movement = Dir;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Dir = new Vector2(0.75f, -0.75f);
                movement = Dir;
            }
            else
            {
                Dir = new Vector2(0, -1);
                movement = Dir;
            }
        } else
        {
            if (Input.GetKey(KeyCode.A))
            {
                Dir = new Vector2(-1, 0);
                movement = Dir;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Dir = new Vector2(1, 0);
                movement = Dir;
            } else
            {
                movement = new Vector2(0, 0);
            }
        }
    }

    void handleMovement()
    {
        myBody.velocity = movement * speed * Time.fixedDeltaTime;
    }

    void stopMovement()
    {
        myBody.velocity = new Vector2(0,0);
    }

    /**
     * talk to NPC that is currently selecting and has a Talkable component (either in children or in main body)
     */
    public void talkToNPC()
    {
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
        else
            hit = Physics2D.Raycast(gameObject.transform.position, GetComponent<PlayerControl>().Dir, Sight);             

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
            else
            {
                detectingObj = gameObject;
                return false;
            }
        }
        else
        {
            detectingObj = gameObject;
            return false;
        }
    }

     /**
     * interact with the interactive object
     * else pickup this object
     */
    public void interact(GameObject interactingObject) {
        if (interactingObject.GetComponent<InteractiveObj>() != null)
        {
            interactingObject.GetComponent<InteractiveObj>().interact();
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
