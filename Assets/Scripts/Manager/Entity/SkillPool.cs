using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class SkillPool : MonoBehaviour
{
    [System.Serializable]
    public struct Projectile
    {
        public int id;
        public GameObject prefab;
        public GameObject flash;
        public int amount;
    }

    public Dictionary<int, Queue<GameObject>> poolDictionary;
    public Dictionary<int, Queue<GameObject>> flashPoolDictionary;
    public List<Projectile> projectileList = new List<Projectile>();

    public void CreatePool(Transform transform)
    {
        foreach (Projectile pool in projectileList)
        {
            Queue<GameObject> skillQueue = new Queue<GameObject>();
            Queue<GameObject> flashQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.amount; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                skillQueue.Enqueue(obj);

                if (pool.flash != null)
                {
                    //GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
                    //obj.transform.SetParent(transform);
                    //obj.SetActive(false);
                    //skillQueue.Enqueue(obj);

                }
                else
                {
                    Debug.Log("pool.flash == null");
                }
            }
            poolDictionary.Add(pool.id, skillQueue);
        }
    }

    public void GetPoolItem(int id, Transform transform)
    {
        if (poolDictionary.ContainsKey(id))
        {
            GameObject obj = poolDictionary[id].Dequeue();
            poolDictionary[id].Enqueue(obj);
            obj.transform.position = transform.position;
            obj.SetActive(true);

        }
        else
        {
            Debug.Log("Can't find Obj");
        }
    }


    public GameObject GetPoolItem(int id)
    {
        if (poolDictionary.ContainsKey(id))
        {
            GameObject obj = poolDictionary[id].Dequeue();
            poolDictionary[id].Enqueue(obj);
            obj.transform.position = transform.position;
            return obj;
        }
        return null;
    }


}
