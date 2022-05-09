using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class cut_scene_dialogue_manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string[] line;
   [SerializeField] DialogueRunner runner;
    void Start()
    {
        line = ES3.Load<string[]>("Condition1", "Star_Ring_Save/myFile.es3");
        for (var i = 0; i < line.Length; i++)
        {
            string[] parts = line[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("loop_time"))
            {
                if (parts[2].Equals("1")) { runner.startNode = "Random_dialogue.End_of_loop"; }
                else { runner.startNode = "Random_dialogue.End_of_loop_1"; }
            }







            // Update is called once per frame
           
        }
    }
    void Update()
    {

    }
}
