using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rat_king : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite with_king;
    public Sprite without_king;
    SpriteRenderer my_sprite;
    private float closeDistance = 12f;
    bool fox_appear=true;
    void Start()
    {
        my_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("glow_stick");

        for (int i = 0; i < taggedObjects.Length; i++)
        {
            
            if (Vector2.Distance(this.transform.position, taggedObjects[i].transform.position) < closeDistance)
            {
                
                fox_appear = false;
            }
            
        }
      
        if (fox_appear) { my_sprite.sprite = with_king; }
        else { my_sprite.sprite = without_king; }



    }
}
