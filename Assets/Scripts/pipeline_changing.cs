using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class pipeline_changing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject moon_lander;
    public RenderPipelineAsset standard;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moon_lander.activeSelf)
        {
            UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset = null;
        }
        else { UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset = standard; }
    }
}
