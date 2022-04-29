using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ControlPanel;
    public GameObject CreditPanel;
    public AudioSource UI_SE;
    public AudioClip clicked;
    public AudioClip buzzer;
    public static bool new_game=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Continue()
    {
        if (ES3.KeyExists("Condition1", "Star_Ring_Save/myFile.es3"))
        {
            UI_SE.PlayOneShot(clicked);
            SceneManager.LoadScene("Main");

        }
        UI_SE.PlayOneShot(buzzer);

    }
    public void NewGame() 
    {
      
        
        /*if (ES3.KeyExists("Condition1"))
        {
            ES3.DeleteFile("SaveFile");
        }*/
        //
        new_game = true;
        //DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Main");
        UI_SE.PlayOneShot(clicked);
    }
    public void ControlButton()
    {
      
        ControlPanel.SetActive(true);
        UI_SE.PlayOneShot(clicked);
    }
    public void ControlEXIT()
    {
        ControlPanel.SetActive(false);
        UI_SE.PlayOneShot(clicked);
    }
    public void CreditButton()
    {
        CreditPanel.SetActive(true);
        UI_SE.PlayOneShot(clicked);
    }
    public void CreditEXIT()
    {
        CreditPanel.SetActive(false);
        UI_SE.PlayOneShot(clicked);
    }
   
}
