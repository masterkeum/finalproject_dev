using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddSkillPool(SkillTable skillData)
    {
        if (Resources.Load<GameObject>(skillData.prefabAddress) == null)
            return;

        Projectile projectile = new Projectile()
        {
            id = skillData.skillId,
            prefab = Resources.Load<GameObject>(skillData.prefabAddress),
            flash = Resources.Load<GameObject>(skillData.prefabFlashAddress),
            amount = 25
        };
        projectileList.Add(projectile);
    }

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

        projectileList.Clear();
    }

    public void GetPoolSkill(int skillId, Transform point, Vector3 direction, int damage)
    {
        // TODO : 가변적으로 추가생성되게 변경

        if (poolDictionary.ContainsKey(skillId))
        {
            GameObject obj = poolDictionary[skillId].Dequeue();
            obj.transform.position = point.position;
            obj.transform.rotation = Quaternion.LookRotation(direction);
            obj.GetComponent<ProjectileMover>().Init(skillId, damage);
            obj.SetActive(true);

            poolDictionary[skillId].Enqueue(obj);

            StartCoroutine(SkillCall(obj));

        }
        else
        {
            Debug.Log($"Can't find Obj : {skillId}");
        }
    }

    public void GetPoolFlash(int skillId, Transform point, Vector3 direction)
    {
        if (flashPoolDictionary.ContainsKey(skillId))
        {
            GameObject obj = flashPoolDictionary[skillId].Dequeue();
            obj.transform.position = point.position;
            obj.transform.rotation = Quaternion.LookRotation(direction);
            obj.SetActive(true);

            flashPoolDictionary[skillId].Enqueue(obj);
            StartCoroutine(FlashCall(obj));
        }
        else
        {
            Debug.Log($"Can't find Obj : {skillId}");
        }
    }

    public void DestroyDicObject(int key)
    {
        if (poolDictionary.ContainsKey(key))
        {
            Queue<GameObject> queue = poolDictionary[key];
            while (queue.Count > 0)
            {
                GameObject obj = queue.Dequeue();
                Destroy(obj);
            }
            poolDictionary.Remove(key);
        }

        if (flashPoolDictionary.ContainsKey(key))
        {
            Queue<GameObject> queue = flashPoolDictionary[key];
            while (queue.Count > 0)
            {
                GameObject obj = queue.Dequeue();
                Destroy(obj);
            }
            flashPoolDictionary.Remove(key);
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
