using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locker : InteractiveObj
{
    string[] parts;
    PlayerBackpack playerBackpack;
    [SerializeField]GameObject hint;
    [SerializeField] AudioClip pickup;
    [SerializeField] AudioSource audio;
    bool taken = false;
    [SerializeField] Animator lock_er;
    private void Start()
    {
        playerBackpack = FindObjectOfType<PlayerBackpack>();
    }
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        if (taken) { player.talkToSelf("Investigate.15"); }
        else
        {
            if (playerBackpack.contains("Access Card".ToLower().Trim()))
            { player.talkToSelf("Investigate.12"); }
            else { player.talkToSelf("Investigate.6"); }
        }
      

    }
    public override void useItem()
    {
        Item currentItem = InventoryGUIControl.currentUnit.items.Peek();
        PlayerControl player = FindObjectOfType<PlayerControl>();

        if (taken) { player.talkToSelf("Investigate.15"); }
        else
        {
            if (currentItem.ItemName.ToLower().Trim().Contains("Access Card".ToLower().Trim()))
            {
               lock_er.Play("locker_moving_up");
                player.ChangeState(player.stateExplore);
                player.talkToSelf("Investigate.14");
                playerBackpack.add(hint);
                audio.PlayOneShot(pickup);
                hint.SetActive(false);
                taken = true;
               
            }
            else
            {
                player.ChangeState(player.stateExplore);
                player.talkToSelf("Investigate.13");
            }
        }
        base.useItem();
    }
}
