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

    private void GlowColorChange(Item item)
    {
        switch (item.grade)
        {
            case ItemGrade.Normal:
                glowEffect.color = new Color(1f, 1f, 1f);
                break;
            case ItemGrade.Magic:
                glowEffect.color = new Color(40/255f, 1f, 35/255f);
                break;
            case ItemGrade.Elite:
                glowEffect.color = new Color(0f,67/255f,1f);
                break;
            case ItemGrade.Rare:
                glowEffect.color = new Color(1f, 115/255f, 0f);
                break;
            case ItemGrade.Epic:
                glowEffect.color = new Color(1f, 1f, 0f);
                break;
            case ItemGrade.Legendary:
                glowEffect.color = new Color(1f, 0f, 0f);
                break;

        }
    }

}
