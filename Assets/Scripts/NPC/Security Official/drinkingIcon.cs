using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drinkingIcon : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private SecurityOfficial securityOfficial;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //set drunk animation
        switch(securityOfficial.drunkLevel)
        {
            case 0:
                anim.SetInteger("drunkLevel", 0);
                break;
            case 1:
                anim.SetInteger("drunkLevel", 1);
                break;
            case 2:
                anim.SetInteger("drunkLevel", 2);
                break;
        }    
    }
}
