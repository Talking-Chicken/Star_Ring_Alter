using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;

public class IntelUnit : MonoBehaviour
{
    [SerializeField, BoxGroup("Intel Info"),ResizableTextArea]private string intelName, intelDes;
    [SerializeField, BoxGroup("Intel Info")] private Sprite intelIcon;

    [SerializeField, BoxGroup("GUI")] private TextMeshProUGUI intelNameText;
    [SerializeField, BoxGroup("GUI")] private Image intelIconImage;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
