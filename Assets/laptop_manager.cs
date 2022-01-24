using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laptop_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject folder;
    public GameObject id_card;
     bool A;
    public bool loading;
    public Camera computer_camera;

    public GameObject loading_bar;
  
    public GameObject loading_back;
    public GameObject drag_object;
    public Sprite folder_icon;
    void Start()
    {
        A = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        float dist = Vector2.Distance(drag_object.transform.position, folder.transform.position);
        if (dist <= 1&&drag_object !=folder)
        {
            folder.GetComponent<SpriteRenderer>().sprite = folder_icon;
            loading_bar.GetComponent<Animator>().Play("loading");
            Destroy(drag_object);

        }

        //Debug.Log(Physics2D.OverlapPoint(mousePos2D));

    }
}
