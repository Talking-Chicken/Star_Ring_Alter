using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Yarn.Unity;

public class laptop_manager : MonoBehaviour
{
    
    public GameObject folder;
    public GameObject id_card;
     bool A;
    public bool loading;
    public Camera computer_camera;

    public GameObject loading_bar;
  
    public GameObject loading_back;
    public GameObject drag_object;
    [SerializeField] GameObject exit_icon;
    public Sprite folder_icon;

    [SerializeField, BoxGroup("access")]
    private GameObject accessFile;
    private AccessCard accessCard;
    [SerializeField] DialogueRunner dialogue;
    void Start()
    {
        A = true;
    }

    
    void Update()
    {
        if (dialogue.IsDialogueRunning)
        { Cursor.lockState = CursorLockMode.Locked; }
        else { Cursor.lockState = CursorLockMode.None; }
        Vector3 mousePos = computer_camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        if (id_card.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("stay_inserted") && A)
        {
            loading_bar.GetComponent<Animator>().Play("loading");
            
            A = false;
        }
   
        if (loading_bar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("finish_loading"))
        {
            loading_back.SetActive(false);
            
            loading = false;
        }
        if (loading_bar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("loading"))
        {
            loading_back.SetActive(true);
            folder.SetActive(true);
            loading = true;
        }

        float dist = 100;
        if (drag_object != null)
            dist = Vector2.Distance(drag_object.transform.position, folder.transform.position);

        if (dist <= 1&&drag_object !=folder)
        {
            folder.GetComponent<SpriteRenderer>().sprite = folder_icon;
            loading_bar.GetComponent<Animator>().Play("loading");

            //if player dragged the correct file, player will get a level up for their access card (if they have any)
            if (drag_object == accessFile)
            {
                accessCard = FindObjectOfType<PlayerBackpack>().getItem("access card").GetComponent<AccessCard>();
                accessCard.level = 2;
                exit_icon.SetActive(true);
                laptop_camera.workdone = true;
            }

            Destroy(drag_object);
        }

        //Debug.Log(Physics2D.OverlapPoint(mousePos2D));

    }
}
