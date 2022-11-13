using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public enum InvestType {Item, Interact, Conversation}
public class InvestIcon : MonoBehaviour
{
    [SerializeField, BoxGroup("GUI")] private Image iconImage;
    [SerializeField, BoxGroup("Icons")] private Sprite itemIcon, interactiIcon,conversation;
    private Dictionary<InvestType, Sprite> investIcons = new Dictionary<InvestType, Sprite>();

    private PlayerControl player;

    public Dictionary<InvestType, Sprite> InvestIcons {get=>investIcons;}
    public Image IconImage {get=>iconImage;}

    void Start() {
        player = FindObjectOfType<PlayerControl>();
        investIcons.Add(InvestType.Item, itemIcon);
        investIcons.Add(InvestType.Interact, interactiIcon);
        investIcons.Add(InvestType.Conversation, conversation);
    }

    void Update() {
        if (player.DetectingObj != null || player.DetectingObj != player.gameObject)
            if (player.DetectingObj.GetComponentInParent<Item>() != null)
                IconImage.sprite = InvestIcons[InvestType.Item];
            else if(player.DetectingObj.GetComponentInParent<Isometric_AI_Render>()!=null)
                IconImage.sprite = InvestIcons[InvestType.Conversation];
            else
                IconImage.sprite = InvestIcons[InvestType.Interact];
    }
}
