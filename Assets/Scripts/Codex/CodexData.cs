using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CodexData
{
    public Hashtable dialogueNodesCount;

    public CodexData(Codex codex)
    {
        dialogueNodesCount = new Hashtable();
        dialogueNodesCount = codex.DialogueNodes;
    }
}
