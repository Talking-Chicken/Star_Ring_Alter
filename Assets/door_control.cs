using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_control : MonoBehaviour
{
    // Start is called before the first frame update
    public bool open;
    public bool close;
    public bool Auto;
    GameObject player;
    private bool enter;
    float lerpDuration = 3;
    private float height;
    float intial_y;
    public float distance;
    string state="closed";
    public int interpolationFramesCount = 60; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    bool complete=true;
    Vector2 intial_position;
    void Start()
    {
        height = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        intial_y = transform.position.y;
        intial_position = transform.position;
        enter = false;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
      
        float dist = Vector2.Distance(player.transform.position, intial_position);
     
        //Debug.Log(complete);
        if (Auto==false&& open && complete && state=="closed")
        {
            StartCoroutine(Lerp());
            state = "opening";
            complete = false;
        }
        if (Auto && dist<=distance && state == "closed")
        {
            StartCoroutine(Lerp());
            state = "opening";
            complete = false;
        }
        if ( dist>distance && complete && state == "open")
        {
            StartCoroutine(Lerp_close());

            state = "closing";

            complete = false;
        }
        /* if (open&&elapsedFrames<interpolationFramesCount)
         {
             float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
             transform.position=new Vector3(transform.position.x, Mathf.Lerp(intial_y, intial_y+height*0.8f, interpolationRatio),this.transform.position.z);
             elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
         }*/

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            close = true;
            open = false;
            enter = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            close = false;
            enter = true;
        }
    }
  
    IEnumerator Lerp()
    {
        float elapsedFrames = 0;
      

        while (elapsedFrames < interpolationFramesCount)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(intial_y, intial_y + height * 0.8f, interpolationRatio), transform.position.z);
            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
            yield return null;

        }
        transform.position = new Vector3(transform.position.x, intial_y+ height * 0.8f, transform.position.z);
        complete = true;
        state = "open";
        open = false;
      

    }
    IEnumerator Lerp_close()
    {
        float elapsedFrames = 0;
        

        while (elapsedFrames < interpolationFramesCount)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(intial_y + height * 0.8f, intial_y, interpolationRatio),transform.position.z);
            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

            yield return null;
        }
        transform.position = new Vector3(transform.position.x, intial_y,transform.position.z);
        complete = true;
        state = "closed";
        open = false;
    }
}
