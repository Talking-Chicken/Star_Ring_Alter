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
    [SerializeField, BoxGroup("Link")] private List<IntelUnit> nextUnits;

    [SerializeField, BoxGroup("GUI")] private TextMeshProUGUI intelNameText;
    [SerializeField, BoxGroup("GUI")] private Image intelIconImage;

    //getters & setters
    public string IntelName { get => intelName;}
    public string IntelDes { get => intelDes; }
    public Sprite IntelIcon { get => intelIcon; }

    public void showIntelUnit()
    {
        intelNameText.text = intelName;
        intelIconImage.sprite = intelIcon;
       
    }

    public void chooseThisUnit()
    {
        IntelGUIControl.CurrentUnit = this;
    }
}
