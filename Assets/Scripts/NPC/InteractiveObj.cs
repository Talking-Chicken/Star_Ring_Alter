using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractiveObj : MonoBehaviour
{
    private StateManager stateManager;

    //COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON     COMMON
    private Vector2 position;
    private Color themeColor;

    [SerializeField] private bool canUse = true, canInven = true, canNeuro = true;

    public DialogueRunner runner;
    [SerializeField] PlayerControl player;

    [SerializeField] private bool isTimeToTalk; //player can only talk to NPC when they have time to talk    [NPC is able to talk to player]

    [SerializeField, Header("Indicator")] GameObject indicator;
    [SerializeField, Range(0,5.0f)] float indicatorYPos; //higher number means more 
    private Vector2 indicatorPos; //indicator position
    private GameObject currentIndicator; //record the newly created indicator

    //getters & setters
    public bool CanUse {get {return canUse;}}
    public bool CanInven {get {return canInven;}}
    public bool CanNeuro {get {return canNeuro;}}

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

    public IEnumerator waitToChangeToExploreState() {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }
}
