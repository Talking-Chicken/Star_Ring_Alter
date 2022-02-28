using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeuroImplantControl : MonoBehaviour
{
    private NeuroImplantApp currentApp; //the app that player is currently selecting
    private List<NeuroImplantApp> downloadedApps = new List<NeuroImplantApp>();
    private NeuroImplantGUI neuroGui;
    void Start()
    {
        neuroGui = GetComponent<NeuroImplantGUI>();
        
    }

    void Update()
    {
        //selecting currentApp through    
    }

    public void fadeToColor(Button button, Color color) {
        Graphic graphic = button.GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }
}
