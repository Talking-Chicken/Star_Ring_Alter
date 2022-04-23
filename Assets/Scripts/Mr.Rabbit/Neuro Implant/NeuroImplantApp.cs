using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public enum NeuroType {IllegalCopy, CatTechnology, RabbitHeavyIndustry}

/**
 * this is the parent class of neuro implant downloadedApps
 * each app will have a name, certain memory storage, and interact function
 */
public class NeuroImplantApp : MonoBehaviour
{
    [SerializeField, Label("data in inspector override the constructor")] public bool isOverride; 

    public string appName;

    [SerializeField] private int memoryStorage;
    [SerializeField] private string description;
    [SerializeField] private string descriptionAfterUse;
    [SerializeField] private Sprite icon;
    [SerializeField] private NeuroType type;

    //getters & setters
    public string APPName {get => appName; set => appName = value;}
    public int MemoryStorage {get {return memoryStorage;} set {memoryStorage = value;}}
    public string Description {get {return description;} set {description = value;}}
    public string DesctiptionAfterUse {get => descriptionAfterUse; set => descriptionAfterUse = value;}
    public Sprite Icon {get {return icon;} private set {icon = value;}}
    public NeuroType Type {get => type; set => type = value;}

    /**
     * to use this neuro implant app, each app has different functions
     */
    public virtual void interact()
    {
        Debug.LogWarning("trying to use the interact funtion of the app " + APPName + ", but this app doesn't have any interact function");
    }

    /**
     * comare to other app, for now, comparing the app name
     * @param other app that is comparing
     * @return 0 if their name is the same, 1 if this app is bigger, -1 if other app is bigger
     */
    public int compareTo(NeuroImplantApp other)
    {
        return this.APPName.ToLower().Trim().CompareTo(other.APPName.ToLower().Trim());
    }

    //draw its GUI on Mr.Rabbit's neuro impant section
    //in other words, change a general neuro implant cell to this specific one
    public void drawCell() {
        //first draw the icon
        Image icon = GetComponentInChildren<Image>();
        if (Icon != null) {
            icon.sprite = Icon;
        } else 
            Debug.LogWarning("haven't set a icon to this neuro implant " + APPName);

        //second set the background to the correct size;
        RectTransform rTransform = GetComponent<RectTransform>();
        rTransform.localScale = Vector3.one;
        
        rTransform.sizeDelta = new Vector2(rTransform.sizeDelta.x * MemoryStorage, rTransform.sizeDelta.y);
    }

    public virtual void useNeuroImplant() {
        
    }
}
