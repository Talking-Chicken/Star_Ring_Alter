using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/**
 * Codex class contains any information that player has recieved
 * it uses a hashtable for nodes name that player has conversation
 */
public class Codex : MonoBehaviour
{
    public Dictionary<string, int> DialogueNodes = new Dictionary<string, int>();

    [SerializeField] private DialogueRunner runner;

    void Start()
    {
        if (start_manager.new_game) { resetDialogueNodesCount();}
        else loadDialogueNodesCount();
        runner.AddFunction("visitCount", 0, delegate (Yarn.Value[] parameters)
        {
            return getNodeVisited();
        });
    }
    
    void Update()
    {
        //reset node
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        //     resetDialogueNodesCount();
    }

    /**
     * if this dialogue node haven't been visited, create new hashtable key for it and set its value to 1
     * else add 1 to the value
     */
    public void setNodeVisited(string key)
    {
        if (DialogueNodes.ContainsKey(key))
            DialogueNodes[key] = (int)DialogueNodes[key] + 1;
        else
            DialogueNodes.Add(key, 1);
    }

    /**
     * {use current node name of normal dialogue system as the key}
     * if this dialogue node haven't been visited, create new hashtable key for it and set its value to 1
     * else add 1 to the value
     */
    public void setNodeVisited()
    {
        if (DialogueNodes == null) Debug.Log("dialogue nodes is null");
        if (runner == null) Debug.Log("dialogue runner is null");
        if (DialogueNodes.ContainsKey(runner.CurrentNodeName))
            DialogueNodes[runner.CurrentNodeName] = (int)DialogueNodes[runner.CurrentNodeName] + 1;
        else
            DialogueNodes.Add(runner.CurrentNodeName, 1);
    }

    /**
     * return number of times that player visited this dialogue node
     * @return number of times that player visited this dialogue node
     */
    public int getNodeVisited(string key)
    {
        return (int) DialogueNodes[key];
    }

    public int getNodeVisited()
    {
        if (DialogueNodes != null) {
            if (!DialogueNodes.ContainsKey(runner.CurrentNodeName))
                setNodeVisited();
            return (int)DialogueNodes[runner.CurrentNodeName];
        } else {
            Debug.Log("dialogue nodes is null");
            return 0;
        }
    }

    public int getNodeVisitedWithNull(string key) {
        if (DialogueNodes != null) {
            if (!DialogueNodes.ContainsKey(key))
                return 0;
            else
                return (int)DialogueNodes[runner.CurrentNodeName];
        }
        return 0;
    }

    /**
     * save dialogue nodes count to a binary file
     */
    public void saveDialgoueNodesCount()
    {
        SaveSystem.saveDialogueNodesCount(this);
    }

    /**
     * load dialogue nodes count from the binary file
     */
    public void loadDialogueNodesCount()
    {
        CodexData data = SaveSystem.loadCodex();

        DialogueNodes = data.dialogueNodesCount;
    }

    /**
     * reset dialogue nodes count, make them all visited count to 0, in binary file
     */
    public void resetDialogueNodesCount()
    {
        DialogueNodes.Clear();
        saveDialgoueNodesCount();
    }
}
