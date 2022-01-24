using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;

/**
 * GUI class of neuro implant, basically for player neuro device only
 * it shows which neuro app player downloaded and what potential neuro app they found but have not implanted yet
 * there's number that records current memory and total memory storage
 */
public class NeuroImplantGUI : MonoBehaviour
{   
    [SerializeField, BoxGroup("memory")] TextMeshProUGUI capacity;
    [SerializeField, BoxGroup("Apps")] private int cloudAppShowNum, implementedAppShowNum;
    [SerializeField, BoxGroup("Apps")] private Button nextGroup, previousGroup;

    List<Button> cloudApps, implementedApps;
    NeuroImplantDevice playerDevice;

    public NeuroImplantApp currentSelectedApp;
    
    void Start()
    {
        playerDevice = FindObjectOfType<PlayerControl>().GetComponent<NeuroImplantDevice>();
    }

    void Update()
    {
        capacity.text = playerDevice.getCurrentMemory()+"/"+playerDevice.getTotalMemory().ToString();
    }
}
