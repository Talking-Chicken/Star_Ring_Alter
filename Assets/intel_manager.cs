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
                    OS[i].SetActive(true);
                }

            }

            if (parts[0].Equals("elevator_unlocked") && parts[2].Equals("TRUE"))
            {

                for (int j = 0; j < OS.Length; j++)
                {
                   lock_down[i].SetActive(true);
                }

            }

        }
    }
}
