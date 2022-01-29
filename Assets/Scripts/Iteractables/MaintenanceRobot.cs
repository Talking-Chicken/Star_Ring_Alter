using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class MaintenanceRobot : InteractiveObj
{
    [SerializeField] GameObject UIContainer;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField, Tooltip("this is the icon for maintenance robot, when it turns into an item")] 
    Sprite itemIcon;

    private PlayerBackpack playerbackpack;
    private StateManager state;
    private Talkable talk;

    [SerializeField, BoxGroup("talking area")]
    private GameObject talkingArea;
    void Start()
    {
        playerbackpack = FindObjectOfType<PlayerBackpack>();
        state = FindObjectOfType<StateManager>();
        talk = GetComponent<Talkable>();
    }

    public override void interact()
    {
        state.transitionState(State.UI);
        UIContainer.SetActive(true);
        if (playerbackpack.contains("operating software"))
            description.text = "use operating software to active the robot?";
        else
            description.text = "this dude is broken";

        //talk about the laptop with Mr.Rabbit
        talk.getPlayer().NPCToTalk = gameObject;
        talk.getPlayer().talkToNPC();
    }

    public void exitUI()
    {
        UIContainer.SetActive(false);
        state.transitionState(State.Explore);
    }

    public void confirm()
    {
        if (playerbackpack.contains("operating software"))
        {
            gameObject.transform.parent.gameObject.AddComponent<MaintenanceRobotItem>();
            GetComponentInParent<MaintenanceRobotItem>().setIcon(itemIcon);
            playerbackpack.remove("operating software");
            UIContainer.SetActive(false);
            state.transitionState(State.Explore);

            talkingArea.SetActive(true);

            Destroy(this);
        }else
        {
            UIContainer.SetActive(false);
        }
        state.transitionState(State.Explore);

    }
}
