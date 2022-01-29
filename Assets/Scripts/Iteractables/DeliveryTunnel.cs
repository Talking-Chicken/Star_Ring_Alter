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

    public void exit()
    {
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
    }

    public void confirm()
    {
        if (playerBackpack.contains("maintenance robot") && cofeeBeanCover.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("stay_up"))
        {
            GameObject robot = playerBackpack.remove("maintenance robot");
            gameObject.AddComponent<MrRabbitTalk>();

            if (talkingArea != null)
                talkingArea.SetActive(true);
        }
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
    }
}
