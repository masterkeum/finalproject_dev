using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINoCurrency : UIBase
{
    public GameObject[] icon;
    public TextMeshProUGUI warningText;


    public void NoActionPoint()
    {
        for(int i = 0; i < icon.Length; i++)
        {
            icon[i].SetActive(false);
        }
        icon[0].SetActive(true);

        warningText.text = "행동력이 부족합니다.";
    }

    public void NoGem()
    {
        for (int i = 0; i < icon.Length; i++)
        {
            icon[i].SetActive(false);
        }
        icon[1].SetActive(true);

        warningText.text = "보석이 부족합니다.";
    }

    public void NoGold()
    {
        for (int i = 0; i < icon.Length; i++)
        {
            icon[i].SetActive(false);
        }
        icon[2].SetActive(true);

        warningText.text = "골드가 부족합니다.";
    }

    public void NoCore()
    {
        for (int i = 0; i < icon.Length; i++)
        {
            icon[i].SetActive(false);
        }
        icon[3].SetActive(true);

        warningText.text = "정수가 부족합니다.";
    }

    public void OnConfirmButton()
    {
        gameObject.SetActive(false);
    }
}
