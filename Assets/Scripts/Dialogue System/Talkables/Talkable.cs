using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * all objects and NPCs that can talk to player need to have this component
 */
public class Talkable : MonoBehaviour
{
    private PlayerControl player;
    private CharacterTraits character;
    private YarnProgram dialogueFile;
    private string startNode;

    void Start()
    {
        if (GetComponentInParent<CharacterTraits>() != null)
        {
            character = GetComponentInParent<CharacterTraits>();
            dialogueFile = character.dialogueFile;
            startNode = character.startNode;
        }  else
        {
            Debug.LogWarning("needs CharacterTraits in parent");
        }

        if (FindObjectOfType<PlayerControl>()) player = FindObjectOfType<PlayerControl>();
        else Debug.LogWarning("cannot find Player Control Script");
    }

    
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null && (player.NPCToTalk == null || player.NPCToTalk == player))
            player.NPCToTalk = character.gameObject;
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null && (player.NPCToTalk == null || player.NPCToTalk == player))
            player.NPCToTalk = character.gameObject;
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null)
            player.NPCToTalk = null;
    }

    /**
     * return dialogue file that is trying to display
     * @return yarn program dialogue file that  this character is going to initiate
     */
    public YarnProgram getDialogueFile()
    {
        return dialogueFile;
    }

    /**
     * return start node of this dialogue file
     * @return start node of this dialogue file
     */
    public string getStartNode()
    {
        return startNode;
    }

    /**
     * using a hashtable : node name is the key, time visited that node is the value
     */

    /**
     * update hashtable value of current visiting node  
     */

    public PlayerControl getPlayer()
    {
        return player;
    }
}
