using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class light_manager : MonoBehaviour
{
    // Start is called before the first frame update

    public Light2D light;
    public Light2D[] light_list;
    float timeElapsed;
    float lerpDuration = 3;
    float startValue = 0;
    float endValue = 10;
    float valueToLerp;
    AudioSource BGM;
    public AudioClip[] BGMs;
    bool once;
    void Start()
    {
        BGM = GetComponent<AudioSource>();
        BGM.clip = BGMs[0];
        BGM.Play();
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time_text.time_2.Minute >= 22)
        {
            if (once)
            {
                BGM.Stop();
                BGM.clip = BGMs[1];
                Debug.Log("bgm changed");
                BGM.Play();
                once = false;
            }
            if (timeElapsed < lerpDuration)
            {
                valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                light.intensity = Mathf.Lerp(1f, 0.2f, timeElapsed);
            }
            for (int i= 0;i<light_list.Length;i++)
            {
               
               
                light_list[i].GetComponent<Light2D>().enabled = true;
            }

        

           

        }
    }
}
