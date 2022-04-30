using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base card class that has a virtual play function that will be called after player play the card
public class Card : MonoBehaviour
{
    protected PlayerControlArcade player;
    private int indexInHand;

    private AudioSource audioSource;
    
    //getters & setters
    public int IndexInHand {get{return indexInHand;} set {indexInHand = value;}}
    
    void Start() {
        player = FindObjectOfType<PlayerControlArcade>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void play() {
        gameObject.SetActive(false);
    }

    //set a indicator at the card position, when mouse is hover on it
    public void examineThisCard() {
        if (player != null && player.CurrentState == player.statePlay)
            player.selectCurrentCard(this);
    }

    //use this card, when player clicked on it
    public void selectThisCard() {
        if (player != null && player.CurrentState == player.statePlay)
        {
            audioSource.Play();
            player.ChangeToMoveState();
        }
    }
}
