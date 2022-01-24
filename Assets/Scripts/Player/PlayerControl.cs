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

    public Vector2 dir { get; private set; } //direction that player is facing [cannot be (0,0)]
    private Vector2 movement = new Vector2(0,0); //direction that player is going [can be (0,0)]

    [Header("Dialogue")] public GameObject NPCToTalk;
    public static bool canTalk = false;
    [SerializeField] public DialogueRunner dialogueRunner;
    [SerializeField] private DialogueUI dialogueUI;

    Rigidbody2D myBody;

    private KeyManager key;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        key = FindObjectOfType<KeyManager>();
    }

    void Update()
    {
        setDir();
        if (canTalk)
        {
            if (Input.GetKeyDown(key.talk) && !dialogueRunner.IsDialogueRunning)
                talkToNPC();
        }
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
                dir = new Vector2(-0.75f, 0.75f);
                movement = dir;
            } else if (Input.GetKey(KeyCode.D))
            {
                dir = new Vector2(0.75f, 0.75f);
                movement = dir;
            }  else
            {
                dir = new Vector2(0, 1);
                movement = dir;
            }
        } else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                dir = new Vector2(-0.75f, -0.75f);
                movement = dir;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir = new Vector2(0.75f, -0.75f);
                movement = dir;
            }
            else
            {
                dir = new Vector2(0, -1);
                movement = dir;
            }
        } else
        {
            if (Input.GetKey(KeyCode.A))
            {
                dir = new Vector2(-1, 0);
                movement = dir;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir = new Vector2(1, 0);
                movement = dir;
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
}
