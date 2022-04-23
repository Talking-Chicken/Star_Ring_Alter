using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class Yarn_show_image : MonoBehaviour
{
    public GameObject[] images;
    public Canvas canvas;
    [YarnCommand("Image")]
    public void Image(string name,string state)
    {
        Debug.Log("test");
        for (int i=0;i<images.Length;i++) { if (images[i].name.Equals(name)) {
                if (state.Equals("true")) { images[i].SetActive(true); }
                else { images[i].SetActive(false); }
               } }
       
    }
}
