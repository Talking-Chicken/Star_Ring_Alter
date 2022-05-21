using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CutsceneTalking : MonoBehaviour
{
    private DialogueRunner runner;

    void Start()
    {
        runner = FindObjectOfType<DialogueRunner>();
        StartCoroutine(waitToStart());
    }

    
    void Update()
    {
        
    }

    IEnumerator waitToStart()
    {
        yield return new WaitForSeconds(1.0f);
        runner.StartDialogue();
    }
}
