using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    public GameObject yellowStar;
    private int _starSlotIndex;
   
    private void Start()
    {
        SetIndex();
        SetYellowStar();
    }

    private void SetYellowStar()
    {

    }

    private void SetIndex()
    {
        _starSlotIndex = transform.GetSiblingIndex();
    }

}
