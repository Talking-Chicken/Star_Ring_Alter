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

    List<GameObject> cloudApps = new List<GameObject>(), downloadedApps = new List<GameObject>();
    NeuroImplantDevice playerDevice;

    //GUI
    [SerializeField, BoxGroup("GUI")] GameObject emptyCell, app;
    private List<GameObject> emptyCells = new List<GameObject>(); //object pool that holds empty cells that created at the start of the game 
    [SerializeField, BoxGroup("GUI")] GameObject appAreaBackground; //background image for apps
    [SerializeField, BoxGroup("GUI")] TextMeshProUGUI nameText, descrtiptionText;

    public NeuroImplantApp currentSelectedApp;

    //getters & setters
    public TextMeshProUGUI NameText {get {return nameText;} set {nameText = value;}}
    public TextMeshProUGUI DescriptionText {get {return descrtiptionText;} set {descrtiptionText = value;}}
    
    void Start()
    {
        playerDevice = FindObjectOfType<PlayerControl>().GetComponent<NeuroImplantDevice>();
        initializeAppArea();
        Debug.Log(emptyCells.Count);
    }

    void Update()
    {
        capacity.text = playerDevice.getCurrentMemory()+"/"+playerDevice.getTotalMemory().ToString();
    }

    /*
    * initialize and update the layout of downloaded app area
    */
    public void initializeAppArea() {
        //create empty cells if there's no empty cell created, for object pool
        if (emptyCells.Count == 0) {
            for (int i = 0; i < 10; i++) { //10 for now
                emptyCells.Add(Instantiate(emptyCell, transform.position, Quaternion.identity));
                emptyCells[i].transform.SetParent(gameObject.transform);
                emptyCells[i].transform.localScale = Vector3.one;
                RectTransform rTransform = appAreaBackground.GetComponent<RectTransform>();
                Debug.Log(rTransform.rect.position.x);
                float xPos = rTransform.position.x - rTransform.rect.width + rTransform.rect.width/5 * i + 10;
                emptyCells[i].GetComponent<RectTransform>().position = new Vector3(xPos,
                                                                                   rTransform.position.y,
                                                                                   0);
            }
        } else {
            for (int i = 0; i < emptyCells.Count; i++) {
                emptyCells[i].SetActive(true);
            }
        }

        //substitute empty cells for downloaded apps
        for (int i = 0; i < playerDevice.downloadedApps.Count; i++) {
            int size = playerDevice.downloadedApps[i].MemoryStorage;

            //de-active empty cells 
            for (int n = i; n < i + size; n++) {
                if (!emptyCells[n].activeSelf)
                    Debug.LogWarning("this empty cells has already de-activated");
                else
                    emptyCells[n].SetActive(false);
            }

            //put downloaded apps to the correct position
            
        }

        
    }
}
