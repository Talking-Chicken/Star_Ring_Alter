using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSystem : MonoBehaviour
{
    private StateManager stateManager;
    private static KeyManager key;
    private RabbitSystemControl rabbit;
    //[SerializeField] private IsometricPlayerMovementController player;
    public GameObject player;
    //backpack of player
    private BackPack playerBackpack;

    //the object that raycasting is hitting
    public GameObject detectingObj;

    [Header("ray casting"), Range(0f, 10.0f), SerializeField] private float sight;

    [Header("indicator"), SerializeField] private GameObject indicator;
    private Vector2 indicatorPos;
    [SerializeField, Range(0f, 3.0f)] private float indicatorYOffset;

    public static bool canInven = true;
    public static bool canOpenBackpack = false;

    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        key = FindObjectOfType<KeyManager>();
        rabbit = FindObjectOfType<RabbitSystemControl>();
        playerBackpack = gameObject.GetComponent<BackPack>();
        player = FindObjectOfType<PlayerControl>().gameObject;
    }

    
    void Update()
    {
        //CAN detect and pickup items
        if (canInven && (stateManager.getCurrentState() == State.Explore)) {
            detectItem();
            updateIndicator();
            interact();
        }
       
        if (stateManager.getCurrentState() == State.Explore || stateManager.getCurrentState() == State.UI)
            playerBackpackOpen();
    }

    /**
     * use raycasting to detect interactable object
     * which contains Item class in their parents
     */
    void detectItem()
    {
        RaycastHit2D hit;
        if (player.GetComponent<IsometricPlayerMovementController>() != null)
            hit = Physics2D.Raycast(player.gameObject.transform.position, player.GetComponent<IsometricPlayerMovementController>().dir, sight);
        else
            hit = Physics2D.Raycast(player.gameObject.transform.position, player.GetComponent<PlayerControl>().dir, sight);             

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponentInParent<Item>() != null)
            {
                detectingObj = hit.collider.gameObject.GetComponentInParent<Item>().gameObject;
            }
            else if (hit.collider.gameObject.GetComponent<InteractiveObj>() != null)
            {
                detectingObj = hit.collider.gameObject.GetComponent<InteractiveObj>().gameObject;
            }
            else
            {
                detectingObj = gameObject;
            }
        }
        else
        {
            detectingObj = gameObject;
        }
    }

    /**
     * open backpack if detectingObj has one
     * else pickup detectingObj
     */
    public void interact()
    {
        if (detectingObj != null && detectingObj != gameObject)
        {
            if (Input.GetKeyDown(key.interact))
            {
                if (detectingObj.GetComponent<InteractiveObj>() != null)
                {
                    detectingObj.GetComponent<InteractiveObj>().interact();
                }
                else if (detectingObj.GetComponent<BackPack>() != null)
                {
                    backpackOpen(detectingObj.GetComponent<BackPack>());
                } else
                {
                    pickUp();
                }
            }
        }
    }

    /**
     * set the position of indicator to position of detectingObj
     */
    public void updateIndicator()
    {
        if (indicator != null)
        {
            if (detectingObj != null && detectingObj != gameObject)
            {
                indicator.SetActive(true);
                indicator.transform.position = new Vector2(detectingObj.transform.position.x, detectingObj.transform.position.y + indicatorYOffset);
            } else
            {
                indicator.SetActive(false);
            }
        }
    }

    /**
     * to open BackPack b
     * then enter UI state
     * 
     * @param b the backpack that ready to open
     */
    public void backpackOpen(BackPack b)
    {
        b.backpackOpen();
        stateManager.transitionState(State.UI);
    }

    /**
     * to cloase Backpack b
     * then exit UI starte
     * 
     * @param b the backpack that ready to close
     */
    public void backpackClose(BackPack b)
    {
        b.backpackClose();
        stateManager.transitionState(State.Explore);
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

    /**
     * go to inventory mode when not in it
     * -----------OR------------
     * exit UI state when already in inventory mode
     * 
     * @control using [key.openBackpack]
     */
    private void playerBackpackOpen()
    {
        if (Input.GetKeyDown(key.openBackpack))
        {
            rabbit.transitionUIState(UIState.Inventory);
        }
    }

}
