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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame() 
    {
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
