using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;

public class cut_scene_dialogue_manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string[] line;
   [SerializeField] DialogueRunner runner;
   public static int cutscene = 0;
    string looptime;
    [SerializeField] GameObject video;
    [SerializeField] GameObject memory_1;
    [SerializeField] GameObject memory_2;
    [SerializeField] AudioSource audio;
    [SerializeField] Camera camera;
    [SerializeField] TextMeshProUGUI interaction_text;
    [SerializeField] AudioClip surgical;
    [SerializeField] AudioClip light_off;
    [SerializeField] AudioClip robot;
    [SerializeField] AudioClip door;
    void Start()
    {
        interaction_text.text = null;
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
        GraphicsSettings.defaultRenderPipeline = null;
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
                    camera.GetComponent<FadeCamera>().FadeOut(2f);
                    audio.Stop();
                    if (looptime.Equals("2"))
                    {
                        StartCoroutine(ExampleCoroutine2());
                    }
                    else if (looptime.Equals("1"))
                    { StartCoroutine(ExampleCoroutine()); }
                    else { SceneManager.LoadScene("Main"); }

                    cutscene = 2;

                    break;//Intro
                }
            case (2):
                {

                    
                    break;//Intro
                }
            case (3):
                {
                    if (looptime.Equals("2"))
                    {
                        runner.startNode = "Random_dialogue.Memory_2";
                        runner.StartDialogue();
                       
                        audio.PlayOneShot(door);
                        cutscene = 4;
                        StartCoroutine(ExampleCoroutine1());
                    }
                    else if (looptime.Equals("1"))
                    {
                        runner.startNode = "Random_dialogue.Memory";
                        //   audio.PlayOneShot(surgical);
                        audio.clip = surgical;
                        audio.Play();
                        runner.StartDialogue();
                        cutscene = 4;
                        StartCoroutine(ExampleCoroutine1());
                    }
            
                        

                    break;//Intro
                }
            case (4):
                {
                   
                    if (runner.IsDialogueRunning==false) {
                        //press space
                        camera.GetComponent<FadeCamera>().FadeIn(2f);

                        
                    }
                    
                    break;//Intro
                }
            case (5):
                {



                    if (looptime.Equals("2"))
                    { interaction_text.text = "Press Space to turn on Desk Lamp"; 
                        cutscene = 6; }
                    else if (looptime.Equals("1"))
                    {
                        interaction_text.text = "Press Space to turn off Surgical Light";
                        cutscene = 6;
                    }
                    
                    break;//Intro
                }
            case (6):
                {

                    if (Input.GetKey(KeyCode.Space))
                    {
                        interaction_text.text = "";
                        if (looptime.Equals("1"))
                        {
                            audio.PlayOneShot(light_off);
                            audio.PlayOneShot(robot);
                        }
                         

                        if (looptime.Equals("2"))
                        {
                            memory_2.GetComponent<Animator>().Play("light_on");
                            audio.PlayOneShot(light_off);
                            runner.startNode = "Random_dialogue.Memory_3";
                        }
                        else if (looptime.Equals("1"))
                        {
                            memory_1.GetComponent<Animator>().Play("memory_1");
                            runner.startNode = "Random_dialogue.Memory_1";
                        }
                           
                        runner.StartDialogue();
                        cutscene = 7;
                    }

                    break;//Intro
                }
            case (7): {
                    //camera.GetComponent<FadeCamera>().FadeOut(2f);
                    break; }
            case (8):
                {
                   // camera.GetComponent<FadeCamera>().FadeOut(2f);
                    break;
                }
        }
    }
    IEnumerator ExampleCoroutine()
    {
        

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(6);
        video.SetActive(false); memory_1.SetActive(true);

        cutscene = cutscene + 1;
    }
    IEnumerator ExampleCoroutine1()
    {


        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(6);
        

        cutscene = cutscene + 1;
    }
    IEnumerator ExampleCoroutine2()
    {


        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(6);
        video.SetActive(false); memory_2.SetActive(true);

        cutscene = cutscene + 1;
    }
}
