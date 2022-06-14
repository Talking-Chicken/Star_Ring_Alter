using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intel_manager : MonoBehaviour
{
    // Start is called before the first frame update
    string[] parts;
    private Codex codex;
    [SerializeField] GameObject[] elevator_door;
    [SerializeField] GameObject[] OS;
    [SerializeField] GameObject[] office;
    [SerializeField] GameObject[] office_1;
    [SerializeField] GameObject[] lock_down;
    [SerializeField] GameObject[] beat_arcade;
    [SerializeField] GameObject[] hint;
    [SerializeField] GameObject[] spy;
    void Start()
    {
        codex = FindObjectOfType<Codex>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (codex != null) {

            if (codex.getNodeVisitedWithNull("MrRabbit.Elevator_Start")>0) 
            {
                for (int i=0;i<elevator_door.Length;i++)
                { 
                    elevator_door[i].SetActive(true);
                }
                
              

            }
            if (codex.getNodeVisitedWithNull("MrRabbit.Manager_Office_Access_Machine_Low_Access_Level") > 0)
            {
                for (int i = 0; i < elevator_door.Length; i++)
                {
                    office[i].SetActive(true);
                }



            }
            if (codex.getNodeVisitedWithNull("MrRabbit.Manager_Office_Access_Machine_No_Card") > 0)
            {
                for (int i = 0; i < elevator_door.Length; i++)
                {
                    if (office_1.Length > i)
                        office_1[i].SetActive(true);
                }



            }

        }
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("check_broken_robot") && parts[2].Equals("TRUE"))
            {

                for (int j = 0; j < OS.Length; j++)
                {
                    OS[j].SetActive(true);
                }

            }

            if (parts[0].Equals("elevator_unlocked") && parts[2].Equals("TRUE"))
            {

                for (int j = 0; j < lock_down.Length; j++)
                {
                   lock_down[j].SetActive(true);
                }

            }

            if (parts[0].Equals("beat_arcade") && parts[2].Equals("TRUE"))
            {

                for (int j = 0; j < beat_arcade.Length; j++)
                {
                    beat_arcade[j].SetActive(true);
                }

            }

            if (parts[0].Equals("hint_checked") && parts[2].Equals("TRUE"))
            {

                for (int j = 0; j < hint.Length; j++)
                {
                    hint[j].SetActive(true);
                }

            }

            if (parts[0].Equals("spy_terminal") && parts[2].Equals("TRUE"))
            {

                for (int j = 0; j < spy.Length; j++)
                {
                    spy[j].SetActive(true);
                }

            }
        }
    }
}
