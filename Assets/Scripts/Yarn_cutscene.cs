using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Yarn_cutscene : MonoBehaviour
{
    [YarnCommand("Cutscene")]
    public void Cutscene()
    {
        
        cut_scene_dialogue_manager.cutscene++;

    }
   
}
