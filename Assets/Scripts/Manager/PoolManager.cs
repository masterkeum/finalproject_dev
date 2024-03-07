using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;


// NOTE : 기존에 개인프로젝트에 사용했던것으로
// 좀더 범용적으로 UnityEngine.Pool 을 사용할 방법이 있는지?


public class PoolManager : SingletoneBase<PoolManager>
{
    private Dictionary<int, IObjectPool<PoolAble>> unitPoolDic = new Dictionary<int, IObjectPool<PoolAble>>();

    private Vector2 minSize = new Vector2(-11, -11);
    private Vector2 maxSize = new Vector2(11, 11);

    protected override void Init()
    {
        //Dictionary<int, UnitData> itemDataDictionary = DataManager.Instance.itemDataDictionary;
        //foreach (int key in itemDataDictionary.Keys)
        //{
        //    //Debug.Log(key);
        //    IObjectPool<PoolAble> pool = new ObjectPool<PoolAble>(() =>
        //    CreatePooledItem(itemDataDictionary[key]), OnGetFromPool
        //        , OnReleaseToPool, OnDestroyPoolObject
        //        , true, itemDataDictionary[key].defaultCapacity, itemDataDictionary[key].maxSize);

        //    if (unitPoolDic.ContainsKey(itemDataDictionary[key].uid))
        //    {
        //        Debug.LogFormat("{0} 이미 등록된 오브젝트입니다.", itemDataDictionary[key].uid);
        //        return;
        //    }
        //    unitPoolDic.Add(itemDataDictionary[key].uid, pool);
        //}
    }

    // 생성
    //private PoolAble CreatePooledItem(UnitData unitData)
    //{
    //    // 프리팹 불러오기
    //    GameObject prefab = Resources.Load<GameObject>(unitData.prefabFilePath);

    //    // 게임 오브젝트 생성해서 리턴
    //    PoolAble poolable = Instantiate(prefab).GetComponent<PoolAble>();
    //    poolable.SetPool(unitPoolDic[unitData.uid]);
    //    return poolable;
    //}

    // 대여
    private void OnGetFromPool(PoolAble poolObj)
    {
        poolObj.gameObject.SetActive(true);
    }

    // 반환
    private void OnReleaseToPool(PoolAble poolObj)
    {
        poolObj.gameObject.SetActive(false);
    }

    // 삭제
    private void OnDestroyPoolObject(PoolAble poolObj)
    {
        Destroy(poolObj.gameObject);
    }

    public PoolAble GetPoolAble(int uid)
    {
        if (unitPoolDic.ContainsKey(uid) == false)
        {
            Debug.LogFormat("{0} 오브젝트풀에 등록되지 않은 오브젝트입니다.", uid);
            return null;
        }

        return unitPoolDic[uid].Get();
    }



    
}

