using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laptop_camera : InteractiveObj
{
    PlayerBackpack playerBackpack;
    StateManager state;
    [SerializeField] GameObject laptop;
    [SerializeField] GameObject laptopPOV;
    private void Start()
    {
        state = FindObjectOfType<StateManager>();
        playerBackpack = FindObjectOfType<PlayerBackpack>();
    }

    public override void interact()
    {
        if (playerBackpack.contains("access card") && !laptop.activeSelf)
        {
            state.transitionState(State.UI);
            laptop.SetActive(true);
            Cursor.visible = false;
            laptopPOV.SetActive(true);
            //Display.displays[1].Activate();
        }
    }
}
