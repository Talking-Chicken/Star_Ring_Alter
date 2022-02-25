using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateInventory : UIStateBase
{
    public override void EnterState(UIControl UI) {
        UI.openInventory();
    }
    public override void UpdateState(UIControl UI) {
        //press inventory or Mr.Rabbit one more time to close it, then change UI state to idle and player state to explore
        if (Input.GetKeyUp(UI.Key.openBackpack) || Input.GetKeyUp(UI.Key.openRabbit)) {
            UI.closeInventory();
            UI.Player.ChangeState(UI.Player.stateExplore);
            UI.ChangeState(UI.stateIdle);
        }
    }
    public override void LeaveState(UIControl UI) {

    }
}