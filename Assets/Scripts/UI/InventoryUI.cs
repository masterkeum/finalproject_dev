using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public void OnClickMimic()
    {
        UIManager.Instance.ShowUI<UIMimicGacha>();
    }
}
