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
    public AudioClip scan;
    public AudioClip scan_SE;
    public AudioClip granted;
    public AudioClip denied;
    private PlayerBackpack playerBackpack;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        playerBackpack = FindObjectOfType<PlayerBackpack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("scan_complete")) //&& m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            if (needAccessCard)
            {
                //unlock door if player has level 2 access
                if (checkAccess(2))
                {
                    //unlock door at end of animation clip scan
                    m_Animator.SetTrigger("complete");
                    audio.PlayOneShot(granted);
                    door.GetComponent<door_control>().open = true;
                } else
                {
                    m_Animator.Play("no_pass");
                    audio.PlayOneShot(denied);
                }
            }
            else
            {
                //unlock door at end of animation clip scan
                m_Animator.SetTrigger("complete");
                audio.PlayOneShot(granted);
                door.GetComponent<door_control>().open = true;
            }
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            m_Animator.SetTrigger("scan");
            audio.PlayOneShot(scan);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player"&& m_Animator.GetCurrentAnimatorStateInfo(0).IsName("scan"))
        {
            m_Animator.Play("no_pass");
            audio.Stop();
            audio.PlayOneShot(denied);
        }
    }

    /**
     * check if players have Item access card that has enough level
     * @param level level of access that player need
     * @return true if player has the access card of that level, false otherwise
     */
    public bool checkAccess(int level)
    {
        Item accessCard = playerBackpack.getItem("access card");
        if (accessCard != null)
            if (accessCard.GetComponent<AccessCard>().level >= level)
                return true;
        return false;
    }
}
