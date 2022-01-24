using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

/**
 * this class present the time system
 * it uses data from TimeLoopSystem
 */
public class TimeGUI : MonoBehaviour
{
    [FormerlySerializedAs("text box for time"), SerializeField] private TextMeshProUGUI timeText;
    
    public bool am_pm_clock;
    [HideInInspector] public bool isAm;

    [SerializeField] private int currentHour, currentMinute;

    private StateManager state;
    void Start()
    {
        state = FindObjectOfType<StateManager>();
    }

    
    void Update()
    {
        if (state.getCurrentState() == State.UI)
        {
            timeText.gameObject.SetActive(true);
            showTime();
            //timeText.text = (int)TimeLoopSystem.currentTime/60 + "";
        } else
        {
            timeText.gameObject.SetActive(false);
        }
    }

    /**
     * set current hour from time loop system, it will alter due to 12 or 24 hour, all float point will round down
     * then, show time in presentation format of [hour : minute a.m./p.m.]
     */
    public void showTime()
    {
        if (TimeLoopSystem.currentTime < 1440) //60 minutes * 24 hours = 1440 minutes (1 day)
            currentHour = (int)TimeLoopSystem.currentTime / 60;
        else
            currentHour = ((int)TimeLoopSystem.currentTime - 1440) / 60;

        if (am_pm_clock)
        {
            if (currentHour > 12)
                timeText.text = currentHour - 12 + " : " + getCurrentMinute() + "p.m.";
            else if (currentHour > 24)
                timeText.text = currentHour - 24 + " : " + getCurrentMinute() + "a.m.";
            else
                timeText.text = currentHour + " : " + getCurrentMinute() + "a.m.";
        } else
        {
            if (currentHour < 24)
                timeText.text = currentHour + " : " + getCurrentMinute();
            else
                timeText.text = currentHour - 24 + " : " + getCurrentMinute();
        }
        
    }

    /**
     * set current minute from time loop system, it will alter due to 12 or 24 hour, all float point will round down
     */
    public int getCurrentMinute()
    {
        return currentMinute = (int)TimeLoopSystem.currentTime % 60;
    }

}
