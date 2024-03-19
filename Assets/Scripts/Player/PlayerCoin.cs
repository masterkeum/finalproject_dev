using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour
{
    private int gold;
    private int exp;
    private bool IsInit = false;
    
    public virtual void Init(int _gold, int _exp)
    {
        if (IsInit) return;
        gold = _gold;
        exp = _exp;
        
        IsInit = true;
    }
    
    
}
