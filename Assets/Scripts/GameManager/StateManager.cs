using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Explore, Dialogue, UI, Setting
}
public class StateManager : MonoBehaviour
{
    public static State currentState = State.Explore;
    public static State previousState = State.Explore;

    public PlayerControl player;

    private bool dialogueFinished = true;

    static float transitionCoolDown; //after player change state, they need to wait this amout of time to change to another state

    private RabbitSystemControl rabbit;

    [Header("opruional test"), SerializeField] Text textToTest;
    void Start()
    {
        transitionCoolDown = 0f;
        rabbit = FindObjectOfType<RabbitSystemControl>();
    }

    void Update()
    {
        inState();


        //test for current state
        if (textToTest != null)
            textToTest.text = getCurrentState().ToString();
    }

    public void transitionState(State newState)
    {
        switch (newState)
        {
            /*
                * it will set UIstate to empty
                */
            case State.Explore:
                if (getCurrentState() != newState)
                {
                    Debug.Log("transitioning to explore");
                    previousState = currentState;
                    currentState = newState;
                    break;
                }
                else
                {
                    Debug.Log("still in exploreing state");
                    break;
                }

            /*
                * it will set UIstate to empty
                */
            case State.Dialogue:
                if (getCurrentState() != newState)
                {
                    previousState = currentState;
                    currentState = newState;
                    break;
                }
                else
                    break;

            /*
                * it will open UI background
                */
            case State.UI:
                if (getCurrentState() != newState)
                {
                    Debug.Log("transitioning to UI");
                    previousState = currentState;
                    currentState = newState;
                    //rabbit.onOpenBackground();
                    break;
                }
                else
                    break;
        }
    }

    void inState()
    {
        switch (currentState)
        {
            case State.Explore:
                player.canMove = true;
                InvenSystem.canInven = true;
                PlayerControl.canTalk = true;
                break;

            case State.Dialogue:
                player.canMove = false;
                InvenSystem.canInven = false;
                PlayerControl.canTalk = false;
                break;

            case State.UI:
                player.canMove = false;
                InvenSystem.canInven = false;
                PlayerControl.canTalk = false;
                break;
        }
    }

    public State getCurrentState()
    {
        return currentState;
    }

    public State getPreviousState()
    {
        return previousState;
    }

    //transit to dialogue state, used for Unity Event System
    public void transitToDialogueState()
    {
        transitionState(State.Dialogue);
    }

    //transit to explore state, used for Unity Event System
    public void transitToExploreState()
    {
        transitionState(State.Explore);
    }

    //transit to UI state, used for unity Event System
    public void transitToUIState()
    {
        transitionState(State.UI);
    }
}

