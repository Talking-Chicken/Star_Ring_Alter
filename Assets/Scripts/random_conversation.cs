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
        if (ES3.KeyExists("Condition1")) {
         
            lines= ES3.Load<string[]>("Condition1");
        }
        else { readCSV(); }
         
    }

    // Update is called once per frame
    void Update()
    {
        if (Time_text.time_2.Minute>=23&&once) {
         player.talkToSelf("Random_dialogue.Time_Remind");
            once = false;
        }
        if (Time_text.time_2.Minute >=23 && Time_text.time_2.Second >= 58&&once1) { 
            player.talkToSelf("Random_dialogue.End_Remind"); 
            once1 = false; 
        }
        lines_2=lines;
    }
    void readCSV()
    {
        lines = file.text.Split("\n"[0]);
        //lines = file.text.Split(System.Environment.NewLine.ToCharArray());
        for (int i = 0; i < lines.Length; i++)
        {
           //parts = lines[i].Split(',');
           
        }
    

    }
}
