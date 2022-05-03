using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcade : InteractiveObj
{
    PlayerBackpack playerBackpack;
    StateManager state;
    [SerializeField] GameObject Arcade;
    [SerializeField] GameObject ArcadePOV;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip clip;
    [SerializeField] GameObject error;
    [SerializeField] GameObject eparts;
    private bool broken = false;
    public static bool once=false;
    public static float score;
    private Talkable talk;
    private void Start()
    {
        state = FindObjectOfType<StateManager>();
        playerBackpack = FindObjectOfType<PlayerBackpack>();
        talk = GetComponent<Talkable>();
    }
    private void Update()
    {
       
        if (score>=14&&broken==false) {

            error.SetActive(true);
            broken = true;
            eparts.SetActive(true);
            PlayerControl player = FindObjectOfType<PlayerControl>();
            player.ChangeState(player.stateExplore);
           
            player.talkToSelf("Response.win_arcade");
        }
        if (once)
        {
            Time_text time_text = FindObjectOfType<Time_text>();
            time_text.addtime1((int)score);
            once = false;
        }
    }
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);

        if (broken) { player.talkToSelf("Response.broken_arcade"); } else
        {
            if (playerBackpack.contains("Token Coin"))
            {
                player.talkToSelf("Response.Arcade_Yes_Coin");
            }
            else
            {
                player.talkToSelf("Response.Arcade_No_Coin");
            }
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
            audio.PlayOneShot(clip);
            for (int i = playerBackpack.backpack.Count - 1; i >= 0; i--)
            {
                if (playerBackpack.backpack[i].GetComponent<Item>().ItemName.ToLower().Trim().Contains("Token Coin".ToLower().Trim()))
                    playerBackpack.backpack.RemoveAt(i);
            }
        }
        else {
            player.ChangeState(player.stateExplore);
            player.talkToSelf("Response.Arcade_wrong_item");
        }
    
}

    public override void useNeuroImplant() {
        
    }

}
