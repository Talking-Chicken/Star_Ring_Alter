using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow_follower : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sprite;
    SpriteRenderer character_sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        character_sprite = this.GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.sortingLayerID = character_sprite.sortingLayerID - 1;
    }
}
