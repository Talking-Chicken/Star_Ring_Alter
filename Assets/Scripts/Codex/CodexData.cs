using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CodexData
{
    public Dictionary<string, int> dialogueNodesCount;

    public CodexData(Codex codex)
    {
        dialogueNodesCount = new Dictionary<string, int>();
        dialogueNodesCount = codex.DialogueNodes;
    }
}
