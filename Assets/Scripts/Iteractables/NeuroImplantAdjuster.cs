using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuroImplantAdjuster : InteractiveObj
{
    private NeuroComputerGUI UI;
    private UIControl uiControl;
    [SerializeField] GameObject adjusterCamera;
    [SerializeField] GameObject adjusterUIContainer;
    private void Start()
    {
        UI = GetComponentInParent<NeuroComputerGUI>();
        uiControl = FindObjectOfType<UIControl>();
    }

    public override void interact()
    {
        // if (UI.isShowingUI)
        // {
        //     UI.hideUI();
        //     uiControl.ChangeState(uiControl.stateIdle);
        //     uiControl.Player.ChangeState(uiControl.Player.stateExplore);
        // } else
        // {
        //     UI.showUI();
        // }
        Camera.main.gameObject.SetActive(false);
        adjusterCamera.SetActive(true);
        adjusterUIContainer.SetActive(true);
    }
}
