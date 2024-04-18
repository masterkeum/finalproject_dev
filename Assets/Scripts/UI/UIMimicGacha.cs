using UnityEngine;
using UnityEngine.UI;

public class UIMimicGacha : UIBase
{
    public Image itemImage;
    public Image glowEffect;

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
                glowEffect.color = ColorTable.normalColor;
                break;
            case ItemGrade.Magic:
                glowEffect.color = ColorTable.magicColor;
                break;
            case ItemGrade.Elite:
                glowEffect.color = ColorTable.eliteColor;
                break;
            case ItemGrade.Rare:
                glowEffect.color = ColorTable.rareColor;
                break;
            case ItemGrade.Epic:
                glowEffect.color = ColorTable.epicColor;
                break;
            case ItemGrade.Legendary:
                glowEffect.color = ColorTable.legendaryColor;
                break;

        }
    }

}
