using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    public GameObject yellowStar;
    public int starSlotIndex;
    public Image starImage;

    private void Start()
    {
        SetIndex();
    }

    public void SetYellowStar(float alpha = 1.0f)
    {
        starImage.color = new Color(starImage.color.r, starImage.color.g, starImage.color.b, alpha);
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
