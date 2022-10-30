using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class light_manager : MonoBehaviour
{
    // Start is called before the first frame update
    bool once = true;
    public Light2D light;
    int interpolationFramesCount = 200; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
  
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        if (Time_text.time_2.Minute >= 22)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
            light.intensity = 0.2f;

           once = false;

        }
    }
}
