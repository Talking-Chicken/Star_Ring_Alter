using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class arcade_narrative : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string[] parts;
    [SerializeField] GameObject arcade_player;
    [SerializeField] GameObject manager;
    [SerializeField] DialogueRunner dialogue;
    [SerializeField] GameObject BGM;
    private bool first_time;
    bool beat;
    void Start()
    {
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("arcade_first_play") && parts[2].Equals("TRUE"))
            {


                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "FALSE" + "," + parts[3];
                first_time = true;
                
            }
            else if (parts[0].Equals("arcade_first_play") && parts[2].Equals("FALSE")) { first_time = false; }

            if (parts[0].Equals("beat_arcade") && parts[2].Equals("TRUE"))
            {

                beat = true;
             
            }

        }
        if (first_time)
        {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Response.arcade_first");
        }
        if (beat) {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Response.arcade_narrative_after_beat");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogue.IsDialogueRunning) { arcade_player.SetActive(true); manager.SetActive(true);BGM.SetActive(true);
            PlayerControl player = FindObjectOfType<PlayerControl>();
            player.ChangeState(player.stateUI);
        }
    }
}
