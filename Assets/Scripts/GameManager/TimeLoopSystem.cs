using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


/**
 * time loop system is a class controlling time loop.
 * it shows current time and the time that triggers the loop [note that only minute will count down]
 * it also manage what data are carried to the next loop
 */
public class TimeLoopSystem : MonoBehaviour
{
    private Time_text time;
    public static int time_loop_count;
    [SerializeField] int endingHour;
   // private string[] parts;
    private DateTime endingTime;
    void Start()
    {
        //load information from last loop
      //  FindObjectOfType<Codex>().loadDialogueNodesCount();

        time = FindObjectOfType<Time_text>();

        endingTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, endingHour, 0);
    }

    void Update()
    {
        //  if (time.time >= endingTime)
        if (Time_text.time_2 >= endingTime)
        {
            reset();
        }
    }

    /**
     * reset the game but save and load from last time loop
     */
    public static void reset()
    {
        
       FindObjectOfType<Codex>().saveDialgoueNodesCount();

        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
           string[] parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("loop_time"))
            {
                int loop = System.Convert.ToInt32(parts[2]);
                loop = loop + 1;
                random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + loop.ToString() + "," + parts[3];
            }

        }
        DateTime today = DateTime.Today;
        Time_text.time_2 = new DateTime(today.Year, today.Month, today.Day, 0, 19, 0);
        ES3.Save("Condition1",random_conversation.lines, "Star_Ring_Save/myFile.es3");
        SceneManager.LoadScene("cut_scene_2");
    }
     
}
