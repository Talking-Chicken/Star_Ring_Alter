using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIStateSelection : UIStateBase
{
    private int currentSelectionIndex;
 
    public override void EnterState(UIControl UI) {
        UI.openSelectionMenu();
        currentSelectionIndex = 0;
        EventSystem.current.SetSelectedGameObject(null);
    }
    public override void UpdateState(UIControl UI) {
        UI.SelectionButtons[currentSelectionIndex].Select();
        if (!UI.Player.InteractingObj.GetComponent<InteractiveObj>().CanInven && !UI.Player.InteractingObj.GetComponent<InteractiveObj>().CanNeuro)
        { UI.IsInvestigateOnly = true;
          UI.QEkeyContainer.SetActive(false);
        } 

        if (!UI.IsInvestigateOnly) {
           UI.QEkeyContainer.SetActive(true);
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
                
                currentSelectionIndex = Mathf.Min(currentSelectionIndex + 1, UI.SelectionButtons.Count - 1);
            } else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) {
                
                currentSelectionIndex = Mathf.Max(currentSelectionIndex - 1, 0);
            }
        }
        

        //select button
        if (Input.GetKeyDown(UI.Key.interact)) {
            UI.SelectionButtons[currentSelectionIndex].onClick.Invoke();
        }

        //exit selection state
        if (Input.GetKeyDown(KeyCode.Escape)) {
            UI.ChangePlayerState(UI.Player.stateExplore);
            UI.ChangeToIdleState();
        }
    }
    public override void LeaveState(UIControl UI) {
        UI.closeSelectionMenu();
        UI.IsInvestigateOnly = false;
    }
}
