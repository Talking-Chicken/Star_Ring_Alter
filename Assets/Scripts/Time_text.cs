using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Time_text : MonoBehaviour
{
    [SerializeField] private GameObject container;
    public DateTime time;
    private TMP_Text textClock;
    public static DateTime time_2;
    public int change_rate;
    bool once;
    int addtime;

    public static bool isTimePaused = false;
    void Awake()
    {
        textClock = GetComponent<TMP_Text>();

        DateTime today = DateTime.Today;
        time = new DateTime(today.Year, today.Month, today.Day, 0, 19, 0);
    }
   
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main_1"))
            isTimePaused = true;

        string minute = LeadingZero(time.Minute);
        string second = LeadingZero(time.Second);
        textClock.text = minute + ":" + second;

        if (time.Minute == 24)
        {
            time = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
            time_2 = time;
        }
    }
    private void FixedUpdate()
    {
        if (!isTimePaused) { 
            if (once) { 
                time = time + new TimeSpan(0, 0, 0, 0, addtime*change_rate*50);
                once = false; 
            } 

            time = time + new TimeSpan(0, 0, 0, 0, change_rate); }
           
        time_2 = time;
    }
    public void addtime1(int time1) {
    
        once = true;
        Debug.Log("once" + time1);
        addtime = time1;
    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
