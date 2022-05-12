using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;

public class IntelUnit : MonoBehaviour
{
    private string intelName, intelDes;
    private Sprite intelIcon;

    [SerializeField, BoxGroup("GUI")] private TextMeshProUGUI intelNameText, intelDesText;
    [SerializeField, BoxGroup("GUI")] private Image intelIconImage;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
