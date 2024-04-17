using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPool : MonoBehaviour
{
    [Serializable]
    public struct Projectile
    {
        public int id;
        public GameObject prefab;
        public int amount;
        public int skillGroup;
    }

    public Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();
    public List<Projectile> projectileList = new List<Projectile>();

    public void AddSkillPool(SkillTable skillData)
    {
        if (Resources.Load<GameObject>(skillData.prefabAddress) == null)
            return;

        Projectile projectile = new Projectile()
        {
            id = skillData.skillId,
            prefab = Resources.Load<GameObject>(skillData.prefabAddress),
            amount = skillData.poolAmount,
            skillGroup = skillData.skillGroup,
        };
        projectileList.Add(projectile);
    }

    public void CreatePool(Transform parentTransform)
    {
        foreach (Projectile pool in projectileList)
        {
            Queue<GameObject> skillQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.amount; i++)
            {
                GameObject skillgo = Instantiate(pool.prefab, parentTransform.position, Quaternion.identity);
                if (pool.skillGroup != 30000030)
                {
                    skillgo.transform.SetParent(parentTransform);
                }
                //skillgo.GetComponent<ProjectileScript>().Init(pool.id, 1);
                skillgo.SetActive(false);
                skillQueue.Enqueue(skillgo);
            }
            poolDictionary.Add(pool.id, skillQueue);
        }

        projectileList.Clear();
    }

    public void GetPoolProjectileSkill(int skillId, Transform point, Vector3 direction, int damage)
    {
        // TODO : 가변적으로 추가생성되게 변경

        if (poolDictionary.ContainsKey(skillId))
        {
            GameObject obj = poolDictionary[skillId].Dequeue();
            if (obj.activeSelf)
            {
                Transform parentTransform = obj.transform.parent.transform;
                poolDictionary[skillId].Enqueue(obj);

                SkillTable skillData = DataManager.Instance.GetSkillTable(skillId);
                obj = Instantiate(Resources.Load<GameObject>(skillData.prefabAddress), parentTransform.position, Quaternion.identity);
                obj.transform.SetParent(parentTransform);
                obj.SetActive(false);

                //Debug.LogWarning($"expanded pool : poolDictionary[{skillId}] : {poolDictionary[skillId].Count}");
            }

            obj.transform.position = point.position;
            obj.transform.rotation = Quaternion.LookRotation(direction);
            obj.GetComponent<ProjectileScript>().Init(skillId, damage);
            obj.SetActive(true);

            poolDictionary[skillId].Enqueue(obj);

            //StartCoroutine(SkillCall(obj));
        }
        else
        {
            Debug.Log($"Can't find Obj : {skillId}");
        }
    }
    public void GetPoolAreaSkill(int skillId, Vector3 Pos, int damage)
    {
        // TODO : 가변적으로 추가생성되게 변경

        if (poolDictionary.ContainsKey(skillId))
        {
            GameObject obj = poolDictionary[skillId].Dequeue();
            if (obj.activeSelf)
            {
                Transform parentTransform = obj.transform.parent.transform;
                poolDictionary[skillId].Enqueue(obj);

                SkillTable skillData = DataManager.Instance.GetSkillTable(skillId);
                obj = Instantiate(Resources.Load<GameObject>(skillData.prefabAddress), parentTransform.position, Quaternion.identity);
                obj.transform.SetParent(parentTransform);
                obj.SetActive(false);

                //Debug.LogWarning($"expanded pool : poolDictionary[{skillId}] : {poolDictionary[skillId].Count}");
            }

            obj.transform.position = Pos;
            obj.GetComponent<AreaDotDamageScript>().Init(skillId, damage);
            obj.SetActive(true);

            poolDictionary[skillId].Enqueue(obj);

            //StartCoroutine(SkillCall(obj));
        }
        else
        {
            Debug.Log($"Can't find Obj : {skillId}");
        }
    }

    public void GetPoolAroundSkill(int skillId, Vector3 Pos, int damage)
    {
        if (poolDictionary.ContainsKey(skillId))
        {
            GameObject obj = poolDictionary[skillId].Dequeue();
            if (obj.activeSelf)
            {
                Transform parentTransform = obj.transform.parent.transform;
                poolDictionary[skillId].Enqueue(obj);

                SkillTable skillData = DataManager.Instance.GetSkillTable(skillId);
                obj = Instantiate(Resources.Load<GameObject>(skillData.prefabAddress), parentTransform.position, Quaternion.identity);
                obj.transform.SetParent(parentTransform);
                obj.SetActive(false);

                //Debug.LogWarning($"expanded pool : poolDictionary[{skillId}] : {poolDictionary[skillId].Count}");
            }

            obj.transform.position = Pos;
            obj.GetComponent<AroundScript>().Init(skillId, damage);
            obj.SetActive(true);

            poolDictionary[skillId].Enqueue(obj);

            //StartCoroutine(SkillCall(obj));
        }
        else
        {
            Debug.Log($"Can't find Obj : {skillId}");
        }
    }

    public void GetPoolGroundStrikeSkill(int skillId, Vector3 enemyPos, int damage)
    {
        // TODO : 가변적으로 추가생성되게 변경

        if (poolDictionary.ContainsKey(skillId))
        {
            GameObject obj = poolDictionary[skillId].Dequeue();
            if (obj.activeSelf)
            {
                Transform parentTransform = obj.transform.parent.transform;
                poolDictionary[skillId].Enqueue(obj);

                SkillTable skillData = DataManager.Instance.GetSkillTable(skillId);
                obj = Instantiate(Resources.Load<GameObject>(skillData.prefabAddress), parentTransform.position, Quaternion.identity);
                obj.SetActive(false);

                //Debug.LogWarning($"expanded pool : poolDictionary[{skillId}] : {poolDictionary[skillId].Count}");
            }

            obj.transform.position = enemyPos;
            //obj.transform.rotation = Quaternion.LookRotation(Vector3.down);
            obj.GetComponent<GroundStrikeScript>().Init(skillId, damage);
            obj.SetActive(true);

            poolDictionary[skillId].Enqueue(obj);

            //StartCoroutine(SkillCall(obj));
        }
        else
        {
            Debug.Log($"Can't find Obj : {skillId}");
        }
    }

    public void GetPoolSkyFallSkill(int skillId, Vector3 enemyPos, int damage)
    {
        // TODO : 가변적으로 추가생성되게 변경

        if (poolDictionary.ContainsKey(skillId))
        {
            GameObject obj = poolDictionary[skillId].Dequeue();
            if (obj.activeSelf)
            {
                Transform parentTransform = obj.transform.parent.transform;
                poolDictionary[skillId].Enqueue(obj);

                SkillTable skillData = DataManager.Instance.GetSkillTable(skillId);
                obj = Instantiate(Resources.Load<GameObject>(skillData.prefabAddress), parentTransform.position, Quaternion.identity);
                obj.transform.SetParent(parentTransform);
                obj.SetActive(false);

                //Debug.LogWarning($"poolDictionary[{skillId}] : {poolDictionary[skillId].Count}");
            }

            obj.transform.position = enemyPos;
            //obj.transform.rotation = Quaternion.LookRotation(Vector3.down);
            obj.GetComponent<ProjectileScript>().Init(skillId, damage);
            obj.SetActive(true);

            poolDictionary[skillId].Enqueue(obj);

            //StartCoroutine(SkillCall(obj));
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
            while (poolDictionary[key].Count > 0)
            {
                GameObject obj = poolDictionary[key].Dequeue();
                Destroy(obj);
            }
            poolDictionary.Remove(key);
        }
    }

    private IEnumerator SkillCall(GameObject skill)
    {
        yield return new WaitForSeconds(3);
        if (skill)
            skill.SetActive(false);
        yield break;
    }

}
