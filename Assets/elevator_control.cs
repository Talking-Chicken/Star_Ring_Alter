using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class elevator_control : InteractiveObj
{
    // Start is called before the first frame update
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
        stateManager.transitionState(State.UI);
        UIContainer.SetActive(true);
        if (!console.isElevatorActivated)
        {
            if (playerNeuroDevice.search(playerNeuroDevice.downloadedApps, "hacking module"))
            {
                if (playerBackpack.contains("e part 0") && playerBackpack.contains("e part 1") && playerBackpack.contains("e part 3"))
                    description.text = "spend 3 e parts to hack this elevator";
                else if (playerBackpack.contains("e part 0") || playerBackpack.contains("e part 1") || playerBackpack.contains("e part 3"))
                    description.text = "I know how to hack it, but I need more e parts";
                else
                    description.text = "I have no e parts to help me hack it";
            }
            else
                description.text = "looks like I don't have access to use this elevator";
        } else
        {
            if (state == 1)
                description.text = "go to the roof?";
            if (state == 2)
                description.text = "go back to store";
        }

    }
    IEnumerator moving_up()
    {
        
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
   
        m_Animator.Play("stop");
        blocker.SetActive(false);
        state = 2;
        tile_roof_top.SetActive(true);
        to_roof_top.SetActive(true);
    }
    IEnumerator moving_down()
    {

        yield return new WaitForSeconds(5);

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
            || ((playerNeuroDevice.search(playerNeuroDevice.downloadedApps, "hacking module")) 
                 && playerBackpack.contains("e part 0") 
                 && playerBackpack.contains("e part 1") 
                 && playerBackpack.contains("e part 3")))
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
}
