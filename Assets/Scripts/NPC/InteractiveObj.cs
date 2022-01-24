using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractiveObj : MonoBehaviour
{
    private StateManager stateManager;


    //CHARACTER     CHARACTER     CHARACTER     CHARACTER     CHARACTER     CHARACTER     CHARACTER
    private CharacterTraits character;
    private string characterName;


    //ITEM     ITEM     ITEM     ITEM     ITEM     ITEM     ITEM     ITEM     ITEM     ITEM     ITEM     ITEM
    private Item item;
    private string itemName;

    //COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON
    private Vector2 position;
    private Color themeColor;

    [Header("Dialogue")] private YarnProgram dialogueFile;
    private string talkingNode = "";
    [SerializeField] DialogueRunner runner;
    [SerializeField] PlayerControl player;

    [SerializeField] private bool isTimeToTalk; //player can only talk to NPC when they have time to talk    [NPC is able to talk to player]

    [SerializeField, Header("Indicator")] GameObject indicator;
    [SerializeField, Range(0,5.0f)] float indicatorYPos; //higher number means more 
    private Vector2 indicatorPos; //indicator position
    private GameObject currentIndicator; //record the newly created indicator

    private void Awake()
    {
        if (gameObject.GetComponentInParent<CharacterTraits>() != null)
            character = GetComponentInParent<CharacterTraits>();
        if (gameObject.GetComponentInParent<Item>() != null)
            item = GetComponentInParent<Item>();
    }

    void Start()
    {

        stateManager = FindObjectOfType<StateManager>();

        indicatorPos = position + new Vector2(0, indicatorYPos);
    }

    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (currentIndicator != null)
            {
                Destroy(currentIndicator);
            }
        }
    }

    public Vector2 getIndicatorPos()
    {
        return indicatorPos;
    }


    public virtual void interact()
    {
        Debug.Log("INTERACT");
    }
}
