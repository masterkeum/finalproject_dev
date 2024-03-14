using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    public GameObject yellowStar;
    public int starSlotIndex;
   
    private void Start()
    {
        SetIndex();
    }

    public void SetYellowStar()
    {
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
