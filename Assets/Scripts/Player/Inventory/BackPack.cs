using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    public List<GameObject> backpack;

    [Header("Backpack UI (optional)")] public GameObject UI;
    void Start()
    {
        backpack = new List<GameObject>();
    }

     void Update()
    {

    }

    /**
     * add item to backpack
     * 
     * @param g gameObject to be added
     */
    public void add(GameObject g)
    {
        backpack.Add(g);
    }

    /**
     * remove item from backpack
     * 
     * @param g gameObject to be removed
     */
    public void remove(GameObject g)
    {
        if (backpack.Count > 0 && backpack.Contains(g))
        {
            backpack.Remove(g);
        }
        else Debug.LogWarning("cannot remove");
    }

    /**
     * remove item base on name
     * @param itemName the name of the item that needs to be removed
     * @return the gameobject that is been removed if remove successfully, null if there's not such item
     */
    public GameObject remove(string itemName)
    {
        if (backpack.Count > 0)
            foreach (GameObject item in backpack)
                if (item.GetComponent<Item>().getName().ToLower().Trim().Equals(itemName))
                {
                    remove(item);
                    return item;
                }
        return null;
    }

    /**
     * remove item from backpack at position index
     * 
     * @param g gameObject to be added
     */
    public void removeAt(int index)
    {
        if (backpack.Count > index)
        {
            backpack.RemoveAt(index);
        }
        else Debug.LogWarning("cannot remove object at " + index);
    }

    /**
     * set active backpack UI
     */
    public void backpackOpen()
    {
        if (UI != null)
            UI.SetActive(true);
        else
            Debug.LogWarning("there's no backpack UI in this object");
    }

    /**
     * 
     */
    public void backpackClose()
    {
        if (UI != null)
            UI.SetActive(false);
        else
            Debug.LogWarning("there's no backpack UI in this object");
    }

    /**
     * check whether backpack contains certain item
     * it will check for Item class fist, then check for item name of the GameObject
     * @param g gameobject that is searching for
     * @return true if g (or item that has the same name as g) is in backpack, false otherwise;
     */
    public bool contains(GameObject g)
    {
        if (g.GetComponent<Item>() != null)
        {
            foreach (GameObject item in backpack)
                if (item.GetComponent<Item>().getName().ToLower().Trim().Equals(g.GetComponent<Item>().getName().ToLower().Trim()))
                    return true;
        }
        return false;
    }

    /**
     * check whether backpack contains item that has certain name
     * @param itemName item name that is checking for
     * @return true if there's an item has the itemName, false otherwise;
     */
    public bool contains(string itemName)
    {
        foreach (GameObject item in backpack)
            if (item.GetComponent<Item>().getName().ToLower().Trim().Equals(itemName.ToLower().Trim()))
                return true;

        return false;
    }
}
