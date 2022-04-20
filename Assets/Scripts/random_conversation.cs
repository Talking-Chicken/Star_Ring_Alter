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
    [SerializeField] PlayerControl player;
    void Start()
    {
        readCSV();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time_text.time_2.Minute>=23&&once) {
         player.talkToSelf("Random_dialogue.Time_Remind");
            once = false;
        }
        lines_2=lines;
    }
    void readCSV()
    {
       lines = file.text.Split("\n"[0]);
        for (var i = 0; i < lines.Length; i++)
        {
           parts = lines[i].Split(","[0]);
           
        }
    

    }
}
