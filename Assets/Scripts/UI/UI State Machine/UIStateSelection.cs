using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStateSelection : UIStateBase
{
    private int currentSelectionIndex;
    public override void EnterState(UIControl UI) {
        currentSelectionIndex = 0;
    }
    public override void UpdateState(UIControl UI) {
        //put indicator to correct position
        UI.SelectionIndicator.transform.position = UI.SelectionButtons[currentSelectionIndex].transform.position;

        if (Input.GetKeyUp(UI.Key.next)) {
            
            currentSelectionIndex = Mathf.Min(currentSelectionIndex + 1, UI.SelectionButtons.Count - 1);
            Debug.Log("next " + currentSelectionIndex);
        } else if (Input.GetKeyUp(UI.Key.previous)) {
            
            currentSelectionIndex = Mathf.Max(currentSelectionIndex - 1, 0);
            Debug.Log("previous " + currentSelectionIndex);
        }
        

        //select button
        if (Input.GetKeyUp(UI.Key.interact)) {
            UI.SelectionButtons[currentSelectionIndex].onClick.Invoke();
        }
    }
    public override void LeaveState(UIControl UI) {
        UI.closeSelectionMenu();
    }
}
