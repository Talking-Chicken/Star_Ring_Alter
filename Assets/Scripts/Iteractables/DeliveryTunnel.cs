using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryTunnel : InteractiveObj
{
    public GameObject tunnelExit;

    [SerializeField] GameObject UIContainer;
    [SerializeField] TextMeshProUGUI description;

    private PlayerBackpack playerBackpack;
    private StateManager state;

    [SerializeField] private GameObject cofeeBeanCover;

    //the area that will turn on at corridor after player put robot into the tunnel, to remind player what's the next move
    [SerializeField] private GameObject talkingArea;

    public bool sentRobot;
    void Start()
    {
        playerBackpack = FindObjectOfType<PlayerBackpack>();
        state = FindObjectOfType<StateManager>();

        if (cofeeBeanCover == null)
            Debug.LogWarning("haven't set coffee bean console yet");
    }

    
    void Update()
    {
        
    }

    public override void interact()
    {
        state.transitionState(State.UI);
        UIContainer.SetActive(true);
        if (playerBackpack.contains("maintenance robot") && cofeeBeanCover.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("stay_up"))
            description.text = "send maintenance robot through this tunnel";
        else
            description.text = "this is the tunnel for coffee beans";
    }

    public override void useItem()
    {
        Item deliveryItem = InventoryGUIControl.currentUnit.items.Dequeue();
        playerBackpack.remove(deliveryItem.gameObject);
        deliveryItem.gameObject.SetActive(true);
        Vector3 exitPos = tunnelExit.transform.position;
        deliveryItem.transform.position = new Vector3(exitPos.x, exitPos.y, 42);
        FindObjectOfType<PlayerControl>().UIControl.ChangeToIdleState();
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
        FindObjectOfType<PlayerControl>().UIControl.closeWindows();
        Debug.Log("using item in delivery tunnel");
    }

    public void exit()
    {
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }

    public void confirm()
    {
        if (playerBackpack.contains("maintenance robot") && cofeeBeanCover.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("stay_up"))
        {
            GameObject robot = playerBackpack.remove("maintenance robot");
            gameObject.AddComponent<MrRabbitTalk>();

            if (talkingArea != null)
                talkingArea.SetActive(true);

            sentRobot = true;
        }
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }
}
