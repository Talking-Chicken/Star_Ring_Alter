using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_conversation : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset file;
    public string[] lines;
    string[] parts;
    [SerializeField] PlayerControl player;
    void Start()
    {
        readCSV();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time_text.time_2.Minute>=11) {

        }
    }
    void readCSV()
    {
       lines = file.text.Split("\n"[0]);
        for (var i = 0; i < lines.Length; i++)
        {
           parts = lines[i].Split(","[0]);
            if (parts[0]== "check_elevator_door") {
                //player.talkToSelf("test");

            }
        }
    

    }
}
