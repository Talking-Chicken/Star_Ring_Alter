using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * GUI of neuro computer, a kind of meuro device, it is read and download only
 */
public class NeuroComputerGUI : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject UIContainer;
    [SerializeField, BoxGroup("computer GUI")] private TextMeshProUGUI appName, appDescription, downloadingState;
    [SerializeField, BoxGroup("player GUI")] private TextMeshProUGUI capacityText;

    private NeuroImplantDevice playerDevice;
    [SerializeField] NeuroImplantDevice computerDevice;
    public NeuroImplantApp currentSelectedApp;
    [SerializeField, BoxGroup("computer GUI")] private List<Button> cloudAppButtons;

    public bool isShowingUI;
    void Start()
    {
        playerDevice = FindObjectOfType<PlayerControl>().GetComponent<NeuroImplantDevice>();
        if (computerDevice == null)
            computerDevice = GetComponent<NeuroImplantDevice>();

        //set up cloud app list
        if (computerDevice.cloudApps.Count > 0)
        {
            for (int i = 0; i < computerDevice.cloudApps.Count; i++)
            {
                GameObject newApp = cloudAppButtons[i].gameObject;

                //神奇代码，终于弄会了，总而言之就是存在list里面的都是component，要把他们变成System.Type才能用appComponent()
                Component appComp = computerDevice.cloudApps[i];
                var appType = appComp.GetType();
                newApp.AddComponent(appType);

                if (newApp.GetComponent<NeuroImplantApp>() != null)
                    cloudAppButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = newApp.GetComponent<NeuroImplantApp>().appName;
            }
            currentSelectedApp = computerDevice.cloudApps[0];
        }
    }
    
    void Update()
    {
        if (isShowingUI)
        {
            if (currentSelectedApp != null)
            {
                appName.text = currentSelectedApp.appName;
                appDescription.text = currentSelectedApp.getDescription();
            }
            capacityText.text = playerDevice.getCurrentMemory() + "/" + playerDevice.getTotalMemory().ToString();
        }

    }

    /**
     * download app from source device to target device
     * it will show whether download sucessfull or not
     * @param sourceDevice the source device that stores app in the cloud
     * @param targetDevice the target device that is going to download the app from source device
     * @app app that the target device is going to download
     */
    public void download(NeuroImplantDevice sourceDevice, NeuroImplantDevice targetDevice, NeuroImplantApp app)
    {
        if (sourceDevice == null)
        {
            Debug.LogWarning("source device cannot be null");
            return;
        }
        if (targetDevice == null)
        {
            Debug.LogWarning("target device cannot be null");
            return;
        }
        if (app == null)
        {
            Debug.LogWarning("app cannot be null");
            return;
        }
        
        if (!sourceDevice.search(sourceDevice.cloudApps, app))
        {
            downloadingState.text = "download fail, cannot find app";
            return;
        }

        if (targetDevice.download(app))
        {
            int randomNum = (int)Random.Range(0, 4);

            //temporary
            if (app.appName.Equals("hacking module")) {
                if (randomNum == 1)
                    downloadingState.text = " Mr. Rabbit: Excellent choice.";
                else if (randomNum == 2)
                    downloadingState.text = "Mr. Rabbit: I don't know how a program runs in human eyes, to me code is like a rabbit digging holes everywhere.";
                else if (randomNum == 3)
                    downloadingState.text = "Mr. Rabbit: I almost got my motherboard fried by Rabbit Heavy Industries' Defensive Matrices once " +
                        "when I tried to infiltrate their server center in Antarctica.";
            }

            if (app.appName.Equals("engineering module"))
            {
                if (randomNum == 1)
                    downloadingState.text = "Mr.Rabbit: Electrical engineering to me is like physiology to you.";
                else if (randomNum == 2)
                    downloadingState.text = "Mr. Rabbit: Search: Unable to identify the engineering structure of my motherboard.";
                else if (randomNum == 3)
                    downloadingState.text = "Mr. Rabbit: Be careful not to be burned by the welding torch, or stuck by the gear, " +
                        "or be, ah having hands is really annoying.";
            }            
        } else
        {
            downloadingState.text = "Mr. Rabbit: Amo, it looks like Rabbit Heavy Industry holds back on us. " +
                "The equipment they sponsored is not the full version. Your neural implant only has a limited amount of storage space.";
        }
    }

    /**
     * download the current selected app from this computer to player device
     */
    public void downloadCurrentAppToPlayer()
    {
        Debug.Log("download current app to player");
        download(computerDevice, playerDevice, currentSelectedApp);
    }

    /**
     * show UI on the screen
     */
    public void showUI()
    {
        isShowingUI = true;
        UIContainer.SetActive(true);
    }

    /**
     * hide UI from the screen
     */
    public void hideUI()
    {
        isShowingUI = false;
        UIContainer.SetActive(false);
    }

    /**
     * detect cursor is over UI element
     * specific it to only respond buttons in cloud app list
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<NeuroImplantApp>() != null)
            currentSelectedApp = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<NeuroImplantApp>();

        Debug.Log("name of current selected app is " + currentSelectedApp.appName);
    }


    //--------------------------teSt-------------------------------
    public void showDebugMessage(string message)
    {
        Debug.Log(message);
    }
}
