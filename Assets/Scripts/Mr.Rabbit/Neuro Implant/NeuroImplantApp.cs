using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * this is the parent class of neuro implant downloadedApps
 * each app will have a name, certain memory storage, and interact function
 */
public class NeuroImplantApp : MonoBehaviour
{
    public string appName;

    [SerializeField] private int memoryStorage;
    [SerializeField] private string description;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void setMemoryStorage(int memory)
    {
        memoryStorage = memory;
    }

    public int getMemoryStorage()
    {
        return memoryStorage;
    }

    public void setDescription(string des)
    {
        description = des;
    }

    public string getDescription()
    {
        return description;
    }

    /**
     * to use this neuro implant app, each app has different functions
     */
    public virtual void interact()
    {
        Debug.LogWarning("trying to use the interact funtion of the app " + appName + ", but this app doesn't have any interact function");
    }

    /**
     * comare to other app, for now, comparing the app name
     * @param other app that is comparing
     * @return 0 if their name is the same, 1 if this app is bigger, -1 if other app is bigger
     */
    public int compareTo(NeuroImplantApp other)
    {
        return this.appName.ToLower().Trim().CompareTo(other.appName.ToLower().Trim());
    }
}
