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

    NeuroImplantDevice playerDevice;

    //GUI
    [SerializeField, BoxGroup("GUI")] GameObject emptyCell, app, container;
    private List<GameObject> emptyCells = new List<GameObject>(); //object pool that holds empty cells that created at the start of the game 
    private List<GameObject> downloadedApps = new List<GameObject>(); //a place to have a reference of object, in order to destroy them
    [SerializeField, BoxGroup("GUI")] GameObject appAreaBackground; //background image for apps
    [SerializeField, BoxGroup("GUI")] TextMeshProUGUI nameText, descrtiptionText;

    public static int currentNeuroAppIndex;

    //getters & setters
    public TextMeshProUGUI NameText {get {return nameText;} set {nameText = value;}}
    public TextMeshProUGUI DescriptionText {get {return descrtiptionText;} set {descrtiptionText = value;}}
    
    void Start()
    {
        playerDevice = FindObjectOfType<PlayerControl>().GetComponent<NeuroImplantDevice>();
    }

    void Update()
    {
        capacity.text = playerDevice.getCurrentMemory()+"/"+playerDevice.getTotalMemory().ToString();
    }

    /*
    * initialize and update the layout of downloaded app area
    */
    public void initializeAppArea() {
        currentNeuroAppIndex = 0;
        //create empty cells if there's no empty cell created, for object pool
        if (emptyCells.Count < 10) {
            for (int i = 0; i < 10; i++) { //10 for now

                //TODO:
                //the position doesn't remain consistance when play it full screen vs. on a small window
                emptyCells.Add(Instantiate(emptyCell, transform.position, Quaternion.identity));
                emptyCells[i].transform.SetParent(container.gameObject.transform);
                emptyCells[i].transform.localScale = Vector3.one;
                RectTransform rTransform = appAreaBackground.GetComponent<RectTransform>();
                Debug.Log(rTransform.rect.position.x);
                float xPos = rTransform.position.x - rTransform.rect.width + rTransform.rect.width/5 * i + 10;
                emptyCells[i].GetComponent<RectTransform>().position = new Vector3(xPos,
                                                                                   rTransform.position.y,
                                                                                   0);

                //set its index
                emptyCells[i].GetComponent<NeuroImplantUnit>().Index = i;
            }
        }

        //reset app area first, so there's nothing left from last opening
        resetAppArea();

        int nextEmptyIndex = 0; //to keep track what cell should next downloaded app be
        //substitute empty cells for downloaded apps
        for (int i = 0; i < playerDevice.downloadedApps.Count; i++) {
            int size = playerDevice.downloadedApps[i].MemoryStorage;

            //de-active empty cells 
            for (int n = nextEmptyIndex; n < nextEmptyIndex + size; n++) {
                if (!emptyCells[n].activeSelf)
                    Debug.LogWarning("this empty cells has already de-activated");
                else
                    emptyCells[n].SetActive(false);
            }

            //put downloaded apps to the correct position
            GameObject newApp = Instantiate(app,emptyCells[nextEmptyIndex + (int)size/2].GetComponent<RectTransform>().position, Quaternion.identity);
            downloadedApps.Add(newApp);
            newApp.transform.SetParent(container.gameObject.transform);

            //set index
            newApp.GetComponent<NeuroImplantUnit>().Index = i;

            //set the new app has the same component as player downloaded component
            Component appComp = playerDevice.downloadedApps[i];
            var appType = appComp.GetType();
            newApp.AddComponent(appType);

            newApp.GetComponent<NeuroImplantApp>().drawCell();

            nextEmptyIndex += size;
        }
    }

    //activate all empty cell and deactivate all neuro app cells, in order to re-draw them
    public void resetAppArea() {
        for (int i = 0; i < emptyCells.Count; i++) {
            emptyCells[i].SetActive(true);
        }

        for (int i = 0; i < downloadedApps.Count; i++) {
            Destroy(downloadedApps[i]);
        }

        currentNeuroAppIndex = 0;
    }

    //present app name and app description in the corresponding area
    public void showAppDetail() {
        nameText.text = playerDevice.downloadedApps[currentNeuroAppIndex].appName;
        descrtiptionText.text = playerDevice.downloadedApps[currentNeuroAppIndex].Description;
    }
}
