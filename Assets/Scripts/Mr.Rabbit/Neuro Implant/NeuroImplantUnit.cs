using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuroImplantUnit : MonoBehaviour
{
    private NeuroImplantApp neuroApp;
    private int index;

    //getters & setters
    public NeuroImplantApp NeuroApp {get {return neuroApp;} set {neuroApp = value;}}
    public int Index {get {return index;} set {index = value;}}

    //make the current select neurp app to this one
    public void chooseThisApp() {
        NeuroImplantGUI.currentNeuroAppIndex = index;
        FindObjectOfType<NeuroImplantGUI>().showAppDetail();
    }
}
