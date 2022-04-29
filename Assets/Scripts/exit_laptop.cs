using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class exit_laptop : MonoBehaviour
{
    [SerializeField] private GameObject main_camera;
    [SerializeField] private GameObject laptop_camera;

    [SerializeField] private GameObject GUI;
    [SerializeField] DialogueRunner dialogue;
    Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    
    void Update()
    {
        Vector3 mousePos = laptop_camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        if (Input.GetMouseButtonDown(0) && collider == Physics2D.OverlapPoint(mousePos2D))
        {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            player.dialogueRunner.Stop();
            main_camera.SetActive(true);
            laptop_camera.SetActive(false);
            GUI.SetActive(false);
            Cursor.visible = true;
      
            //transit back to explore state
            FindObjectOfType<PlayerControl>().ChangeState(FindObjectOfType<PlayerControl>().stateExplore);

            //transit back to UI idle state
            FindObjectOfType<UIControl>().ChangeState(FindObjectOfType<UIControl>().stateIdle);
           
        }
    }
}
