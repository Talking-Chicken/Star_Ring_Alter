using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    [Header("Key")]
    public KeyCode walkingUP;
    public KeyCode walkingDown, walkingLeft, walkingRight, interact, escape, openRabbit, openBackpack, 
                   openMap, openStatus, openNeuro, continueDialogue, talk, next, previous;

    public static KeyCode Default_WalkingUp = KeyCode.W,
                          Default_WalkingDown = KeyCode.S,
                          Default_WalkingLeft = KeyCode.A,
                          Default_WalkingRight = KeyCode.D,
                          Default_Interact = KeyCode.F,
                          Default_Escape = KeyCode.Escape,
                          Default_OpenRabbit = KeyCode.Tab,
                          Default_OpenBackpack = KeyCode.I,
                          Default_OpenMap = KeyCode.M,
                          Default_OpenStatus = KeyCode.C,
                          Default_ContinueDialgoue = KeyCode.Space,
                          Default_Talk = KeyCode.Space,
                          Default_Next = KeyCode.E,
                          Default_Previous = KeyCode.Q,
                          Default_NeuroImplant = KeyCode.N;
                        

    private void Start()
    {
        FindObjectOfType<Codex>().loadDialogueNodesCount();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.U))
        {
            FindObjectOfType<Codex>().saveDialgoueNodesCount();

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
