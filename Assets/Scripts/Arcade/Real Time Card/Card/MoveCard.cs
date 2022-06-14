using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : Card
{
    [SerializeField] private bool isMovingRight;
    [SerializeField] int distance;
    public override void play()
    {
        FindObjectOfType<PlayerControlArcade>().setDestination(isMovingRight, distance);
        base.play();
    }
}
