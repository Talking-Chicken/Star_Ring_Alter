using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed = 10.0f;

    //getters & setters
    public Vector2 Direction {get {return direction;} set {direction = value;}}

    void Update()
    {
        transform.Translate(Direction * Time.deltaTime * speed);
    }
}
