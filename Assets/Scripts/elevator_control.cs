using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class elevator_control : InteractiveObj
{
    public GameObject to_corridor;
    public GameObject to_roof_top;
    public GameObject tile_corridor;
    public GameObject tile_roof_top;
    public GameObject blocker;
    Animator m_Animator;
    private int state;//0=moving up 1=first floor 2=second floor 3=moving down
    
    [SerializeField, BoxGroup("control access")]
    private ManagerConsole console;

    private PlayerBackpack playerBackpack;
    private NeuroImplantDevice playerNeuroDevice;
    private StateManager stateManager;
    public AudioClip machine;
    public AudioSource sound_effect;
        

    [SerializeField, BoxGroup("UI")] GameObject UIContainer;
    [SerializeField, BoxGroup("UI")] TextMeshProUGUI description;
    void Start()
    {
        state = 1;
        m_Animator = this.GetComponent<Animator>();

        if (console == null)
            Debug.LogWarning("haven't set consle yet");

        playerBackpack = FindObjectOfType<PlayerBackpack>();
        playerNeuroDevice = FindObjectOfType<PlayerControl>().GetComponent<NeuroImplantDevice>();
        stateManager = FindObjectOfType<StateManager>();
    }

    public override void interact()
    {
        //stateManager.transitionState(State.UI);
       // UIContainer.SetActive(true);
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        if (!console.isElevatorActivated)
        {
            if (playerNeuroDevice.search(playerNeuroDevice.downloadedApps, "hacking module"))
            {
                if (getElectroniCount() >= 3)
                    player.talkToSelf("Response_player_action.interact_elevator.7");
                else if (getElectroniCount() >= 1)
                    player.talkToSelf("Response_player_action.interact_elevator.6");
                else
                    player.talkToSelf("Response_player_action.interact_elevator.5");
            }
            else
                player.talkToSelf("Response_player_action.interact_elevator.4");
        } else
        {
            if (state == 1)
            {
              player.talkToSelf("Response_player_action.interact_elevator.2");
                state = 0;
                m_Animator.Play("up");
                StartCoroutine(moving_up());
                sound_effect.PlayOneShot(machine);
                tile_corridor.SetActive(false);
                tile_roof_top.SetActive(false);
                blocker.SetActive(true);
                to_corridor.SetActive(false);
                to_roof_top.SetActive(false);

            }
            if (state == 2)
            {
              player.talkToSelf("Response_player_action.interact_elevator.3");
                state = 3;
                m_Animator.Play("down");
                sound_effect.PlayOneShot(machine);
                StartCoroutine(moving_down());
                tile_corridor.SetActive(false);
                tile_roof_top.SetActive(false);
                blocker.SetActive(true);
                to_corridor.SetActive(false);
                to_roof_top.SetActive(false);
            }
            }

    }

    public override void useItem()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        player.talkToSelf("Response_player_action.interact_elevator.1");
        base.useItem();
    }

    public override void useNeuroImplant()
    {
        NeuroImplantApp app = NeuroGUIControl.currentUnit.NeuroApp;
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.ChangeState(player.stateExplore);
        if (!console.isElevatorActivated)
        {
            if (app.GetComponent<HackingModule>() != null)
            {

                if (getElectroniCount() >= 3)
                {
                    for (int i = playerBackpack.backpack.Count - 1; i >= 0; i--)
                    {
                        if (playerBackpack.backpack[i].GetComponent<Item>().ItemName.ToLower().Trim().Contains("electronic components"))
                            playerBackpack.backpack.RemoveAt(i);
                    }

                    if (state == 1)
                    {
                        state = 0;
                        m_Animator.Play("up");
                        StartCoroutine(moving_up());
                        sound_effect.PlayOneShot(machine);
                        tile_corridor.SetActive(false);
                        tile_roof_top.SetActive(false);
                        blocker.SetActive(true);
                        to_corridor.SetActive(false);
                        to_roof_top.SetActive(false);
                    }
                    if (state == 2)
                    {
                        state = 3;
                        m_Animator.Play("down");
                        sound_effect.PlayOneShot(machine);
                        StartCoroutine(moving_down());
                        tile_corridor.SetActive(false);
                        tile_roof_top.SetActive(false);
                        blocker.SetActive(true);
                        to_corridor.SetActive(false);
                        to_roof_top.SetActive(false);
                    }

                    console.isElevatorActivated = true;

                    player.talkToSelf("Response_player_action.interact_elevator.10");

                }
                else
                {
                    player.talkToSelf("Response_player_action.interact_elevator.9");
                }
            }
            else
            {
                player.talkToSelf("Response_player_action.interact_elevator.8");
            }
        }
        else {
            player.talkToSelf("Response.elevator_unlocked");
        }
        base.useNeuroImplant();
    }
    IEnumerator moving_up()
    {
        
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
   
        m_Animator.Play("stop");
        blocker.SetActive(false);
        state = 2;
        tile_roof_top.SetActive(true);
        to_roof_top.SetActive(true);
    }
    IEnumerator moving_down()
    {

        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.

        m_Animator.Play("stop");
        blocker.SetActive(false);
        state = 1;
        tile_corridor.SetActive(true);
        to_corridor.SetActive(true);
    }

    public void confirm()
    {
        if (console.isElevatorActivated 
            || (playerNeuroDevice.search(playerNeuroDevice.downloadedApps, "hacking module") 
            &&  getElectroniCount() >= 3))
        {
            if (state == 1)
            {
                state = 0;
                m_Animator.Play("up");
                StartCoroutine(moving_up());
                sound_effect.PlayOneShot(machine);
                tile_corridor.SetActive(false);
                tile_roof_top.SetActive(false);
                blocker.SetActive(true);
                to_corridor.SetActive(false);
                to_roof_top.SetActive(false);
            }
            if (state == 2)
            {
                state = 3;
                m_Animator.Play("down");
                sound_effect.PlayOneShot(machine);
                StartCoroutine(moving_down());
                tile_corridor.SetActive(false);
                tile_roof_top.SetActive(false);
                blocker.SetActive(true);
                to_corridor.SetActive(false);
                to_roof_top.SetActive(false);
            }
            UIContainer.SetActive(false);
            stateManager.transitionState(State.Explore);
            FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
        }
    }

    public void exit()
    {
        UIContainer.SetActive(false);
        stateManager.transitionState(State.Explore);
        FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);
    }

    public int getElectroniCount() {
        int ePartCount = 0;
            for (int i = 0; i < playerBackpack.backpack.Count; i++) {
                if (playerBackpack.backpack[i].GetComponent<Item>().ItemName.ToLower().Trim().Contains("electronic components"))
                    ePartCount++;
            }
        return ePartCount;
    }
}
