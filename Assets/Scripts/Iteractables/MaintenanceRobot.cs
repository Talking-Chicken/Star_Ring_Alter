using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaintenanceRobot : InteractiveObj
{
    [SerializeField] GameObject UIContainer;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField, Tooltip("this is the icon for maintenance robot, when it turns into an item")] 
    Sprite itemIcon;

    private PlayerBackpack playerbackpack;
    private StateManager state;
    void Start()
    {
        playerbackpack = FindObjectOfType<PlayerBackpack>();
        state = FindObjectOfType<StateManager>();
    }

    public override void interact()
    {
        state.transitionState(State.UI);
        UIContainer.SetActive(true);
        if (playerbackpack.contains("operating software"))
            description.text = "use operating software to active the robot?";
        else
            description.text = "this dude is broken";
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
            Destroy(this);
        }else
        {
            UIContainer.SetActive(false);
        }
        state.transitionState(State.Explore);

    }
}
