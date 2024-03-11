using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameScene : MonoBehaviour
{
    private void Awake()
    {
        // 풀 생성
        _ = DataManager.Instance;
        //_ = PoolManager.Instance;

        //_ = GameManager.Instance;
    }


}
