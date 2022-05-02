using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcade : InteractiveObj
{
    PlayerBackpack playerBackpack;
    StateManager state;
    [SerializeField] GameObject Arcade;
    [SerializeField] GameObject ArcadePOV;


    private Talkable talk;
    private void Start()
    {
        state = FindObjectOfType<StateManager>();
        playerBackpack = FindObjectOfType<PlayerBackpack>();
        talk = GetComponent<Talkable>();
    }
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        



        if (playerBackpack.contains("Token Coin"))
        {
            player.talkToSelf("Response.Arcade_Yes_Coin");
        }
        else 
        {
            player.talkToSelf("Response.Arcade_No_Coin");
        }
    }
     
    
    public override void useItem()
    {
        Item currentItem = InventoryGUIControl.currentUnit.items.Peek();
        PlayerControl player = FindObjectOfType<PlayerControl>();





        if (currentItem.ItemName.ToLower().Trim().Contains("Token Coin".ToLower().Trim()))
        {
            player.ChangeState(player.stateUI);
            Arcade.SetActive(true);
            // Cursor.visible = false;
            ArcadePOV.SetActive(true);

        }
        else {
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Response.Arcade_wrong_item");
        }
    
}}
