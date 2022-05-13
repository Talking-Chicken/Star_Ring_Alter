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
        if (Input.GetKeyUp(UI.Key.openRabbit) || Input.GetKeyUp(KeyCode.Escape))
        {
            UI.closeGUI();
            UI.Player.ChangeState(UI.Player.stateExplore);
            UI.ChangeState(UI.stateIdle);
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
