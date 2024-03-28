using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMimicGacha : UIBase
{
    public Image itemImage;
    public Image glowEffect;


    private void Awake()
    {

    }

    private void OnEnable()
    {
        string path = GameManager.Instance.accountInfo.newItem.ImageFile;
        itemImage.sprite = Resources.Load<Sprite>(path);
        GlowColorChange(GameManager.Instance.accountInfo.newItem);
    }

    public void ClosePopup()
    {
        GameManager.Instance.accountInfo.GetANewItem();
        gameObject.SetActive(false);

    }

    private void GlowColorChange(ItemTable item)
    {
        switch (item.grade)
        {
            case "Normal":
                glowEffect.color = new Color(1f, 1f, 1f);
                break;
            case "Magic":
                glowEffect.color = new Color(40/255f, 1f, 35/255f);
                break;
            case "Elite":
                glowEffect.color = new Color(0f,67/255f,1f);
                break;
            case "Rare":
                glowEffect.color = new Color(1f, 115/255f, 0f);
                break;
            case "Epic":
                glowEffect.color = new Color(1f, 1f, 0f);
                break;
            case "Legendary":
                glowEffect.color = new Color(1f, 0f, 0f);
                break;

        }
    }

}
