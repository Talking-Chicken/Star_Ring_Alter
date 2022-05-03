using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//this class keep track of score in float and present it in a integer form
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, deathText;
    [SerializeField] private GameObject arcade1;
    [SerializeField] private GameObject arcade_camera;

    [SerializeField] private GameObject main_camera;
    private float score = 1.0f;

    private PlayerControlArcade player;
    private PlayerControl main_player;


    //getter
    public float Score {get {return score;}}

    void Start() {
        player = FindObjectOfType<PlayerControlArcade>();
        main_player= FindObjectOfType<PlayerControl>();
    }
    void Update()
    {
        if (player != null) {
            score += Time.deltaTime;
            scoreText.text = ((int)score).ToString();
        } else {
            deathText.gameObject.SetActive(true);
            deathText.text = "score: "+scoreText.text+"\npress Q to Quit";
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                arcade1.SetActive(false);
                arcade_camera.SetActive(false);
                arcade.score = score;
                main_camera.SetActive(true);
               
                main_player.ChangeState(main_player.stateExplore);
                //transit back to UI idle state
                FindObjectOfType<UIControl>().ChangeState(FindObjectOfType<UIControl>().stateIdle);

            }
        }

        
    }
}
