using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineEnemy : Enemy
{
    private bool isMovingRight = false;
    [SerializeField] private float frenquency, magnitude;

    public bool IsMovingRight {get {return isMovingRight;} set {isMovingRight = value;}}

    public override void move()
    {
        Vector3 pos = transform.position;

         if (IsMovingRight)
            pos += transform.right * Time.deltaTime * base.speed;
        else
            pos -= transform.right * Time.deltaTime * base.speed;

        transform.position = pos + transform.up * Mathf.Sin(base.elapsedTime * frenquency) * magnitude;
    }
}
