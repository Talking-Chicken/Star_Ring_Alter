using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateMap : UIStateBase
{
    public override void EnterState(UIControl UI)
    {
        UI.openGUI += UI.openMap;
        UI.closeGUI += UI.closeMap;
        UI.openGUI();
    }

    public override void UpdateState(UIControl UI)
    {
        if (UI.Player.CanUI) {
            if (Input.GetKeyUp(UI.Key.openRabbit) || Input.GetKeyUp(KeyCode.Escape))
            {
                UI.closeGUI();
                UI.Player.ChangeState(UI.Player.stateExplore);
                UI.ChangeState(UI.stateIdle);
            }
        }

        if (Input.GetKeyDown(UI.Key.previous)) {
            UI.ChangeState(UI.stateNeuro);
        }

        UI.zoomMap();
        UI.moveMap();
        //UI.lockMapCameraZ();
    }

    public override void LeaveState(UIControl UI)
    {
        UI.closeGUI();
        UI.openGUI -= UI.openMap;
        UI.closeGUI -= UI.closeMap;
    }
}
