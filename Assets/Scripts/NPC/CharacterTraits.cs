using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CharacterTraits : MonoBehaviour
{
    public string characterName;
    public Color themeColor;
    
    [Space]
    public Vector2 position;

    [Header("dialogue")] 
    public YarnProgram dialogueFile;
    public string startNode;
    public YarnProgram dialogueBubbleFile;
    public string bubbleTalkingNode;

    [Header("portraits")]
    public Sprite idle;
    public Sprite smile;

    [Space]
    public string testString;
    private void Awake()
    {
        position = gameObject.transform.position;
    }
}
