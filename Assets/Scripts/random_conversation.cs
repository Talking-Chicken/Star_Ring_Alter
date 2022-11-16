using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_conversation : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static string[] lines;
    public TextAsset file;
    [SerializeField] string[] lines_2;
    string[] parts;
    bool once = true;
    bool once1 = true;
    [SerializeField] PlayerControl player;
    void Start()
    {
        if (start_manager.new_game) {
            readCSV();
            FindObjectOfType<Codex>().resetDialogueNodesCount();
            start_manager.new_game = false;
        }
        else {
            lines = ES3.Load<string[]>("Condition1", "Star_Ring_Save/myFile.es3");
            Debug.Log("load key");
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        if (Time_text.time_2.Minute>=22&&once) {
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Random_dialogue.Time_Remind");
            once = false;
        }
        if (Time_text.time_2.Minute >=22 && Time_text.time_2.Second >= 58&&once1) {
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Random_dialogue.End_Remind"); 
            once1 = false; 
        }
        lines_2=lines;
    }
    void readCSV()
    {
        lines = file.text.Split("\n"[0]);
        //lines = file.text.Split(System.Environment.NewLine.ToCharArray());
       /* for (int i = 0; i < lines.Length; i++)
        {
           //parts = lines[i].Split(',');
           
        }*/
    

    }
}
