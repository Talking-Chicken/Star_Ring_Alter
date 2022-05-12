using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminal_system : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject door;

    public bool needAccessCard;
    public AudioSource audio;
    public AudioSource system;
    bool once;
    public AudioClip scan;
    public AudioClip scan_SE;
    public AudioClip granted;
    public AudioClip denied;
    public AudioClip siren;
    bool grant;
    string[] parts;
    private PlayerBackpack playerBackpack;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        playerBackpack = FindObjectOfType<PlayerBackpack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("standby"))
        {
            once = true;
        }
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("scan_complete")) //&& m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            if (needAccessCard)
            {
                //unlock door if player has level 2 access
                if (checkAccess(2))
                {
                    
                    //unlock door at end of animation clip scan
                    m_Animator.SetTrigger("complete");
                    if (once) { system.PlayOneShot(granted); }
                    once = false;
                    door.GetComponent<door_control>().open = true;
                } else
                {
                    m_Animator.Play("no_pass");
                    system.PlayOneShot(denied);
                    audio.PlayOneShot(siren);
                }
            }
            else
            {
                //unlock door at end of animation clip scan
                m_Animator.SetTrigger("complete");

                if (once) { system.PlayOneShot(granted); }
                once = false;
                door.GetComponent<door_control>().open = true;
            }
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            m_Animator.SetTrigger("scan");
            system.PlayOneShot(scan);
            audio.PlayOneShot(scan_SE);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player"&& m_Animator.GetCurrentAnimatorStateInfo(0).IsName("scan"))
        {
            m_Animator.Play("no_pass");
            audio.Stop();
            system.PlayOneShot(denied);
            audio.PlayOneShot(siren);
        }
    }

    /**
     * check if players have Item access card that has enough level
     * @param level level of access that player need
     * @return true if player has the access card of that level, false otherwise
     */
    public bool checkAccess(int level)
    {
        for (var i = 0; i < random_conversation.lines.Length; i++)
        {
            parts = random_conversation.lines[i].Split(',');
            parts[0] = parts[0].Replace("\r", "");

            if (parts[0].Equals("sec_station_checked") )
            {
                
                if (parts[2].Equals("TRUE"))
                {
                   

                    grant = true;
                  //  Debug.Log("test" + grant);
                    break;
                }

            }
           

        }
        Item accessCard = playerBackpack.getItem("access card");
        if (grant) { return true; }
        else
        {
            if (accessCard != null)
                if (accessCard.GetComponent<AccessCard>().level >= level)
                    return true;
            return false;
        }
    }
}
