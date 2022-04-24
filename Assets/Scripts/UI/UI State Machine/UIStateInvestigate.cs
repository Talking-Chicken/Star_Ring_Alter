using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateInvestigate : UIStateBase
{
    public override void EnterState(UIControl UI)
    {
        if (UI.Player.DetectingObj.GetComponent<InteractiveObj>() != null)
            UI.Player.DetectingObj.GetComponent<InteractiveObj>().interact();
    }

    public override void UpdateState(UIControl UI)
    {

    }

    public override void LeaveState(UIControl UI)
    {
        
    }
}
