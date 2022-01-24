using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.EventSystems;

/**
 * this is the device class of neuro implant
 * it have a total memory storage, a list of downloadedApps, and a list of cloud apps that downloaded into this device
 * at first, player don't have any cloud apps, they can only download from other computers
 */
public class NeuroImplantDevice : MonoBehaviour
{
    //downloaded apps are apps that people is using 
    [Tooltip("downloaded apps are apps that people is using")]
    public List<NeuroImplantApp> cloudApps;

    [Tooltip("enable it means it can dowload apps from other devices")]
    public bool downloadAllowed;
    
    [SerializeField, EnableIf("downloadAllowed")] private int totalMemory, currentMemory;
    // cloud apps are apps that player needs to download, in order to use
    [Tooltip("cloud apps are apps that player needs to download, in order to use")]
    [EnableIf("downloadAllowed")] public List<NeuroImplantApp> downloadedApps;
    void Start()
    {

    }

    
    void Update()
    {
        
    }


    /**
     * download the app into this device, return true if downloaded successfully, false otherwise
     * it will be false if
     * 1. currentMemory + app memoryStorage > totalMemory
     * @return whether downloaded the app successfully or not
     */
    public bool download(NeuroImplantApp app)
    {
        Debug.Log("check memory storage");
        if (app == null)
            Debug.LogError("app that you are trying to download cannot be null");
        else
        {
            if (currentMemory + app.getMemoryStorage() > totalMemory)
                return false;
            else
            {
                downloadedApps.Add(app);
                currentMemory += app.getMemoryStorage();
                return true;
            }
        }
        return false;
    }

    /**
     * delete the app from this device, return true if delete successfully, false otherwise
     * it will be false if
     * 1. there's no such app in this device
     * @return whether deleted the app successfully or not
     */
    public bool delete(NeuroImplantApp app)
    {
        if (app == null)
            Debug.LogError("app that you are trying to delete cannot be null");
        else
        {
            if (downloadedApps.Contains(app))
            {
                downloadedApps.Remove(app);
                currentMemory -= app.getMemoryStorage();
                return true;
            }
            else
                Debug.LogWarning("app " + app.name + " that you are trying to delete is not in apps list");
        }
        return false;
    }

    /**
     * upload apps to cloud, so player can download them
     * it will delete the app from download list and put it in cloud list (if applicatable)
     * return false if the app is already in the cloud
     * @param app that is going to be uploaded
     * @return false if a identical app is already in the cloud
     */
    public bool upload(NeuroImplantApp app)
    {
        if (cloudApps.Contains(app))
            return false;
        else
        {
            if (downloadedApps.Contains(app))
                delete(app);
            cloudApps.Add(app);
            return true;
        }
    }

    /**
     * search whether cloud or download apps contains thia app
     * @param appList the list that player want to search, can be either downloadApps or cloudApps
     * @param app that players want to search for
     * @return true if app is in the list, false otherwise
     */
    public bool search(List<NeuroImplantApp> appList, NeuroImplantApp app)
    {
        for (int i = 0; i < appList.Count; i++)
        {
            if (appList[i].compareTo(app) == 0)
                return true;
        }
        return false;
    }

    /**
     * the default function of search, if the parameter only has app, it will automatically search download list
     * @param app that players want to search inside download list
     * @return true if app is in the list, false otherwise
     */
    public bool search(NeuroImplantApp app)
    {
        return search(cloudApps, app);
    }

    public void setTotalMemory(int memory)
    {
        totalMemory = memory;
    }

    public int getTotalMemory()
    {
        return totalMemory;
    }

    public void setCurrentMemory(int memory)
    {
        currentMemory = memory;
    }

    public int getCurrentMemory()
    {
        return currentMemory;
    }

}
