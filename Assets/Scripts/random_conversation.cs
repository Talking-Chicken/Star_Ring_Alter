using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_conversation : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset file;
    public string[] lines;
    void Start()
    {
        readCSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void readCSV()
    {
       lines = file.text.Split("\n"[0]);
        for (var i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(","[0]);
        }
    }
}
