using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class access_manager : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerBackpack playerBackpack;
    public int access_level;
    void Start()
    {
        playerBackpack = FindObjectOfType<PlayerBackpack>();
    }

    // Update is called once per frame
    void Update()
    {
        Item accessCard = playerBackpack.getItem("access card");
        
        if (accessCard != null)
        {
            access_level = accessCard.GetComponent<AccessCard>().level;
            
                    }
    }
           
}
