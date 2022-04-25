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
    private DateTime endingTime;
    void Start()
    {
        //load information from last loop
        FindObjectOfType<Codex>().loadDialogueNodesCount();

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

        FindObjectOfType<StateManager>().transitionState(State.Explore);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);

        DateTime today = DateTime.Today;
        Time_text.time_2 = new DateTime(today.Year, today.Month, today.Day, 0, 19, 0);
        ES3.Save("Condition1",random_conversation.lines);
        SceneManager.LoadScene("cut_scene_2");
    }
}
