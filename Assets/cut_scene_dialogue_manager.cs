using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class cut_scene_dialogue_manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string[] line;
   [SerializeField] DialogueRunner runner;
   public static int cutscene = 0;
    string looptime;
    [SerializeField] GameObject video;
    [SerializeField] GameObject memory_1;
    [SerializeField] AudioSource audio;
    [SerializeField] Camera camera;
    void Start()
    {
        line = ES3.Load<string[]>("Condition1", "Star_Ring_Save/myFile.es3");
        for (var i = 0; i < line.Length; i++)
        {
            string[] parts = line[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("loop_time"))
            {
                looptime = parts[2];
              
            }
            // Update is called once per frame

        }
    }
    private void Update()
    {
        Cutscene();
    }
    void Cutscene()
    {
        switch (cutscene)
        {
            case (0):  
                {
                    if (looptime.Equals("1")) { runner.startNode = "Random_dialogue.End_of_loop"; }
                    else { runner.startNode = "Random_dialogue.End_of_loop_1"; }
                   // cutscene = 1;
                    break;//Intro
                }

            case (1):
                {
                  
                    video.SetActive(false);memory_1.SetActive(true);
                    cutscene = 2;
                    break;//Intro
                }
            case (2):
                {

                    camera.GetComponent<FadeCamera>().FadeOut(2f);
                    runner.startNode = "Random_dialogue.Memory";
                    runner.StartDialogue();
                    cutscene = 3;
                    break;//Intro
                }
            case (3):
                {

                    if (runner.IsDialogueRunning==false) { 
                        //press space
                        cutscene = 4; }
                    
                    break;//Intro
                }
            case (4):
                {

                    if (Input.GetKey(KeyCode.Space))
                    {
                       memory_1.GetComponent<Animator>().Play("memory_1");
                        runner.startNode = "Random_dialogue.Memory_1";
                        runner.StartDialogue();
                        cutscene = 5;
                    }

                    break;//Intro
                }
            case (5): { break; }
        }
    }
}
