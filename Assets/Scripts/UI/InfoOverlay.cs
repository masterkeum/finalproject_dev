using UnityEngine;
using UnityEngine.UI;

public class InfoOverlay : UIBase
{
    public Image image;

    private void OnEnable()
    {
        switch (GameManager.Instance.infoGraphic)
        {
            case InfoGraphic.Inventory:
                image.sprite = Resources.Load<Sprite>("Textures/info/inven");
                break;
            case InfoGraphic.Main:
                image.sprite = Resources.Load<Sprite>("Textures/info/main");
                break;
            case InfoGraphic.Shop:
                image.sprite = Resources.Load<Sprite>("Textures/info/shop");
                break;
            default:
                Debug.LogError("UI Error");
                break;
        }
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }
}
