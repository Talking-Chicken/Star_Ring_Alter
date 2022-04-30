using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//this class keep track of score in float and present it in a integer form
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, deathText;
    private float score = 1.0f;

    private PlayerControlArcade player;

    //getter
    public float Score {get {return score;}}

    void Start() {
        player = FindObjectOfType<PlayerControlArcade>();
    }
    void Update()
    {
        if (player != null) {
            score += Time.deltaTime;
            scoreText.text = ((int)score).ToString();
        } else {
            deathText.gameObject.SetActive(true);
            deathText.text = "score: "+scoreText.text+"\npress R to restart";
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }
}
