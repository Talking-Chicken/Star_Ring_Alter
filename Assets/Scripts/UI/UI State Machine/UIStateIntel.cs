using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateIntel : UIStateBase
{
    public override void EnterState(UIControl UI)
    {
        UI.openGUI += UI.openIntel;
        UI.closeGUI += UI.closeIntel;
        UI.openGUI();
    }

    public override void UpdateState(UIControl UI)
    {
        if (UI.Player.CanUI) {
            if (Input.GetKeyUp(UI.Key.openRabbit) || Input.GetKeyUp(KeyCode.Escape))
            {
                UI.closeGUI();
                UI.ChangeState(UI.stateIdle);
                UI.Player.ChangeState(UI.Player.stateExplore);
            }
        }

        if (Input.GetKeyDown(UI.Key.next)) {
            UI.ChangeState(UI.stateInventory);
        }

        UI.moveIntel();
    }

    public override void LeaveState(UIControl UI)
    {
        UI.closeGUI();
        UI.openGUI -= UI.openIntel;
        UI.closeGUI -= UI.closeIntel;
    }
}
