using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;

public class IntelGUIControl : MonoBehaviour
{
    [SerializeField, BoxGroup("Intel Units")] private List<IntelUnit> units;

    [SerializeField, BoxGroup("GUI")] private TextMeshProUGUI nameText, desText;
    [SerializeField, BoxGroup("GUI")] private Image intelImage;

    public static IntelUnit CurrentUnit;

    void Start()
    {
        CurrentUnit = units[0];
    }

    
    void Update()
    {
        nameText.text = CurrentUnit.IntelName;
        desText.text = CurrentUnit.IntelDes;
        intelImage.sprite = CurrentUnit.IntelIcon;
    }

    public void showIntelUnits()
    {
        foreach (IntelUnit unit in units)
        {
            if (unit.gameObject.activeSelf)
                unit.showIntelUnit();
        }
    }
}
