using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    public GameObject yellowStar;
    public int starSlotIndex;
    private Image starImage;

    private void Awake()
    {
        starImage = yellowStar.GetComponent<Image>();
        yellowStar.SetActive(false);
    }

    private void Start()
    {
        SetIndex();
    }

    public void SetYellowStar(float alpha = 1f)
    {
        if (starImage != null)
        {
            starImage = yellowStar.GetComponent<Image>();
        }
        Color color = starImage.color;
        color.a = alpha;
        starImage.color = color;

        yellowStar.SetActive(true);
    }

    public void ClearYellowStar()
    {
        yellowStar.SetActive(false);
    }

    private void SetIndex()
    {
        starSlotIndex = transform.GetSiblingIndex();
    }

}
