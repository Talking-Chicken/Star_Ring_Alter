using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class Yarn_show_image : MonoBehaviour
{
    public Image[] images;
    public Canvas canvas;
    [YarnCommand("Image")]
    public void Image(string name,string state)
    {
        Debug.Log("test");
        for (int i=0;i<images.Length;i++) { if (images[i].name.Equals(name)) { images[i].enabled = true; } }
       
    }
}
