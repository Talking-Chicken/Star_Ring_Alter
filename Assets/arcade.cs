using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcade : InteractiveObj
{
    PlayerBackpack playerBackpack;
    [SerializeField] GameObject Arcade;
    [SerializeField] GameObject ArcadePOV;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip clip;
    [SerializeField] GameObject error;
    [SerializeField] GameObject[] eparts;
    private bool broken = false;
    public static bool once = false;
    public static float score;
    private Talkable talk;
    private string[] parts;
    bool beat;
    private void Start()
    {
        beat = false;
        playerBackpack = FindObjectOfType<PlayerBackpack>();
        talk = GetComponent<Talkable>();
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("beat_arcade") && parts[2].Equals("TRUE"))
            {

                beat = true;
                break;
            }


        }
    }
    private async void Update()
    {
        if (once)
        {
            //Time_text time_text = FindObjectOfType<Time_text>();
           // time_text.addtime1((int)score);
            once = false;
            if (score >= 40)
            {

                error.SetActive(true);
                broken = true;
                for(int i=0;i<eparts.Length;i++){eparts[i].SetActive(true);}
                
                PlayerControl player = FindObjectOfType<PlayerControl>();
                player.ChangeState(player.stateExplore);
                if (beat) { player.talkToSelf("Response.win_arcade_beat"); } else { player.talkToSelf("Response.win_arcade"); }
                

                for (var i = 0; i < random_conversation.lines.Length; i++)
                {
                    parts = random_conversation.lines[i].Split(',');
                    parts[0] = parts[0].Replace("\r", "");

                    if (parts[0].Equals("beat_arcade") && parts[2].Equals("FALSE"))
                    {


                        random_conversation.lines[i] = parts[0] + "," + parts[1] + "," + "TRUE" + "," + parts[3];

                        break;
                    }


                }
            }

            if (score < 40)
            {

                PlayerControl player = FindObjectOfType<PlayerControl>();
                player.ChangeState(player.stateExplore);
                if (beat) { player.talkToSelf("Response.arcade_finish_game_beat"); } else { player.talkToSelf("Response.arcade_finish_game"); }
               
            }
        }

    }
    public override void interact()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);

        if (broken) { player.talkToSelf("Response.broken_arcade"); }
        else
        {
            if (beat) { player.talkToSelf("Response.arcade_check_if_beat"); }

            else
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



    }


    public override void useItem()
    {
        Item currentItem = InventoryGUIControl.currentUnit.items.Peek();
        PlayerControl player = FindObjectOfType<PlayerControl>();

        if (broken) { player.talkToSelf("Response.broken_arcade"); }
        else
        {



            if (currentItem.ItemName.ToLower().Trim().Contains("Token Coin".ToLower().Trim()))
            {
                player.ChangeState(player.stateUI);
                Arcade.SetActive(true);
                // Cursor.visible = false;
                ArcadePOV.SetActive(true);
                audio.PlayOneShot(clip);
                arcade_narrative.start = true;
                for (int i = playerBackpack.backpack.Count - 1; i >= 0; i--)
                {
                    if (playerBackpack.backpack[i].GetComponent<Item>().ItemName.ToLower().Trim().Contains("Token Coin".ToLower().Trim())) { playerBackpack.backpack.RemoveAt(i); break; }
                        
                }
            }
            else
            {
                player.ChangeState(player.stateExplore);
                player.talkToSelf("Response.Arcade_wrong_item");
            }
        }
        base.useItem();
    }

    public override void useNeuroImplant()
    {
        NeuroImplantApp app = NeuroGUIControl.currentUnit.NeuroApp;
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        if (broken) { player.talkToSelf("Response.broken_arcade"); }
        else
        {

            if (app.GetComponent<HackingModule>() != null)
            {
                if (beat)
                {
                    player.ChangeState(player.stateExplore);
                    player.talkToSelf("Response.arcade_hacking_beat");
                    error.SetActive(true);
                    broken = true;
                    for(int i=0;i<eparts.Length;i++){eparts[i].SetActive(true);}
                }
                else
                {
                    player.ChangeState(player.stateExplore);
                    player.talkToSelf("Response.arcade_hacking_no_beat");
                }
            }
            else
            {
                player.ChangeState(player.stateExplore);
                player.talkToSelf("Response.neural_arcade_wrong");
            }


        }
        base.useNeuroImplant();
    }
}
