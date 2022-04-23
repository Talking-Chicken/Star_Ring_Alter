using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeuroUnit : MonoBehaviour
{
    private string neuroName, neuroDes, neuroDesAfterUse;
    private NeuroImplantApp neuroApp;
    private Sprite icon;
    private int index;
    [SerializeField] private Image unitIconDisplayer;

    private PlayerControl player;

    //getter & setter
    public NeuroImplantApp NeuroApp {get => neuroApp; set => neuroApp = value;}
    public int Index {get => index; set => index = value;}
    public Sprite Icon {get => icon; set => icon = value;}
    public string NeuroName {get => neuroName; set => neuroName = value;}
    public string NeuroDes {get => neuroDes; set => neuroDes = value;}
    public string NeuroDesAfterUse {get => neuroDesAfterUse; set => neuroDesAfterUse = value;}

    void Start() {
        player = FindObjectOfType<PlayerControl>();
    }

    public void resetDisplayingApp() {
        neuroName = "";
        NeuroDes = "";
        NeuroDesAfterUse = "";
        icon = null;
    }

    public void showUnit()
    {
        if (Icon != null) {
            unitIconDisplayer.color = new Color(255,255,255,255);
            unitIconDisplayer.sprite = icon;
        }
        else
            unitIconDisplayer.color = new Color(0,0,0,0);
    }

    /**
     * set current unit of inventory GUI to this one
     */
    public void chooseThisUnit()
    {
        NeuroGUIControl.currentUnit = this;
        FindObjectOfType<NeuroGUIControl>().CurrentIndex = Index;
    }

    public void useNeuroImplant() 
    {
        if (NeuroApp != null) {
            if (player.InteractingObj == null) {
                NeuroApp.useNeuroImplant();
            } else {
                player.InteractingObj.GetComponent<InteractiveObj>().useNeuroImplant();
            }
        }
    }
}
