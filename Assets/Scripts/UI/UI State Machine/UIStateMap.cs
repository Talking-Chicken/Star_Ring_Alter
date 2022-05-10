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
        
        if (Input.GetKeyDown(UI.Key.next)) {
            UI.ChangeState(UI.stateInventory);
        } else if (Input.GetKeyDown(UI.Key.previous)) {
            UI.ChangeState(UI.stateNeuro);
        }

        UI.zoomMap();
        
    }

    public override void LeaveState(UIControl UI)
    {
        UI.closeGUI();
        UI.openGUI -= UI.openMap;
        UI.closeGUI -= UI.closeMap;
    }
}
