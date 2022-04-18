using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateNeuro : UIStateBase
{
    public override void EnterState(UIControl UI) {
        UI.openGUI += UI.openNeuro;
        UI.closeGUI += UI.closeNeuro;
        UI.openGUI();
    }
    public override void UpdateState(UIControl UI) {
        //press Neuro Implant or Mr.Rabbit one more time to close it, then change UI state to idle and player state to explore
        if (Input.GetKeyUp(UI.Key.openNeuro) || Input.GetKeyUp(UI.Key.openRabbit)) {
            UI.closeNeuro();
            UI.Player.ChangeState(UI.Player.stateExplore);
            UI.ChangeState(UI.stateIdle);
        }
    }
    public override void LeaveState(UIControl UI) {
        UI.closeGUI();
        UI.openGUI -= UI.openNeuro;
        UI.closeGUI -= UI.closeNeuro;
    }
}
