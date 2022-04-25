﻿using System.Collections;
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
        if (Input.GetKeyDown(UI.Key.previous)) {
            UI.ChangeState(UI.stateMap);
        }
    }

    public override void LeaveState(UIControl UI)
    {
        UI.closeGUI();
        UI.openGUI -= UI.openIntel;
        UI.closeGUI -= UI.closeIntel;
    }
}
