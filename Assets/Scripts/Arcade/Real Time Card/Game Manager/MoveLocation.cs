using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the reference location for player and enemy to move
public class MoveLocation : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
