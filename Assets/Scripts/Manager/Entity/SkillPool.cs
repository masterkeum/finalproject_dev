using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        poolDictionary = new Dictionary<int, Queue<GameObject>>();
        flashPoolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (Projectile pool in projectileList)
        {
            Queue<GameObject> skillQueue = new Queue<GameObject>();
            Queue<GameObject> flashQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.amount; i++)
            {
                GameObject skillgo = Instantiate(pool.prefab, transform.position, Quaternion.identity);
                skillgo.transform.SetParent(transform);
                skillgo.SetActive(false);
                skillgo.GetComponent<ProjectileMover>().Init(pool.id, 1);
                skillQueue.Enqueue(skillgo);

                if (pool.flash != null)
                {
                    GameObject flashgo = Instantiate(pool.flash, transform.position, Quaternion.identity);
                    flashgo.transform.SetParent(transform);
                    flashgo.SetActive(false);
                    flashQueue.Enqueue(flashgo);
                }
                else
                {
                    Debug.LogWarning("pool.flash == null");
                }
            }
            poolDictionary.Add(pool.id, skillQueue);
            flashPoolDictionary.Add(pool.id, flashQueue);
        }
    }

    public void GetPoolSkill(int id, int level, Transform point, Vector3 direction)
    {
        // TODO : 가변적으로 추가생성되게 변경

        if (poolDictionary.ContainsKey(id))
        {
            GameObject obj = poolDictionary[id].Dequeue();
            obj.transform.position = point.position;
            obj.transform.rotation = Quaternion.LookRotation(direction);
            obj.GetComponent<ProjectileMover>().Init(id, 1);
            obj.SetActive(true);

            poolDictionary[id].Enqueue(obj);

            StartCoroutine(SkillCall(obj));

        }
        else
        {
            Debug.Log($"Can't find Obj : {id}");
        }
    }

    public void GetPoolFlash(int id, Transform point, Vector3 direction)
    {
        if (flashPoolDictionary.ContainsKey(id))
        {
            GameObject obj = flashPoolDictionary[id].Dequeue();
            obj.transform.position = point.position;
            obj.transform.rotation = Quaternion.LookRotation(direction);
            obj.SetActive(true);

            flashPoolDictionary[id].Enqueue(obj);
            StartCoroutine(FlashCall(obj));
        }
        else
        {
            Debug.Log($"Can't find Obj : {id}");
        }
    }

    private IEnumerator SkillCall(GameObject skill)
    {
        yield return new WaitForSeconds(3);
        skill.SetActive(false);
        yield break;
    }

    private IEnumerator FlashCall(GameObject flash)
    {
        yield return new WaitForSeconds(1);
        flash.SetActive(false);
        yield break;
    }

}
