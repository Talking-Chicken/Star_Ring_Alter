using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuroImplantAdjuster : InteractiveObj
{
    private NeuroComputerGUI UI;
    private UIControl uiControl;
    private void Start()
    {
        UI = GetComponentInParent<NeuroComputerGUI>();
        uiControl = FindObjectOfType<UIControl>();
    }

    public override void interact()
    {
        if (UI.isShowingUI)
        {
            UI.hideUI();
            uiControl.ChangeState(uiControl.stateIdle);
            uiControl.Player.ChangeState(uiControl.Player.stateExplore);
        } else
        {
            UI.showUI();
        }
    }
}
