using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuroImplantAdjuster : InteractiveObj
{
    private NeuroComputerGUI UI;
    private UIControl uiControl;
    [SerializeField] GameObject adjusterCamera;
    [SerializeField] GameObject adjusterUIContainer;
    string[] parts;
    bool first;
    private void Start()
    {
        UI = GetComponentInParent<NeuroComputerGUI>();
        uiControl = FindObjectOfType<UIControl>();
    }

    public override void interact()
    {
        // if (UI.isShowingUI)
        // {
        //     UI.hideUI();
        //     uiControl.ChangeState(uiControl.stateIdle);
        //     uiControl.Player.ChangeState(uiControl.Player.stateExplore);
        // } else
        // {
        //     UI.showUI();
        // }
        PlayerControl player = FindObjectOfType<PlayerControl>();
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("hacking_module") && parts[2].Equals("FALSE"))
            {
                first = true;
                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "TRUE" + "," + parts[3];
                break;
            }
            else if (parts[0].Equals("hacking_module") && parts[2].Equals("TRUE")) { first = false; break; }
        }
        if (first) {
            player.ChangeState(player.stateExplore);
            player.talkToSelf("adjuster_first");
        } else {
            player.ChangeState(player.stateUI);
            player.talkToSelf("Response.neural_implant_adjuster");


            Camera.main.gameObject.SetActive(false);
            adjusterCamera.SetActive(true);
            adjusterUIContainer.SetActive(true);
        }
        
    }
}
