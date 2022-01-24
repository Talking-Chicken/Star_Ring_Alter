using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuroImplantAdjuster : InteractiveObj
{
    private NeuroComputerGUI UI;
    private StateManager state;
    private void Start()
    {
        UI = GetComponentInParent<NeuroComputerGUI>();
        state = FindObjectOfType<StateManager>();
    }

    public override void interact()
    {
        if (UI.isShowingUI)
        {
            UI.hideUI();
            state.transitionState(State.Explore);
        } else
        {
            UI.showUI();
            state.transitionState(State.UI);
        }
    }
}
