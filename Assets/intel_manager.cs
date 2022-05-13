using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intel_manager : MonoBehaviour
{
    // Start is called before the first frame update
    string[] parts;
    private Codex codex;
    void Start()
    {
        codex = FindObjectOfType<Codex>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (codex != null) {
            //if(codex.getNodeVisited("")){}
        }
    }
}
