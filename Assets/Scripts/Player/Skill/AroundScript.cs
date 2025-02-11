using System.Collections;
using UnityEngine;

public class AroundScript : MonoBehaviour
{
    private float disableAfterTime = 2f;

    public Vector3 rotateVector = new Vector3(0, 0, 180f);

    private Transform parentObject;
    private LayerMask enemyLayerMask;

    SkillTable skillInfo;
    private int damage;


    public GameObject rotateObjectPrefab;

    public int numberOfObjects = 1; // 돌고 있는 자식 객체의 개수
    public float orbitRadius = 2.4f; // 중심으로부터의 거리
    //public float orbitSpeed = 30f; // 돌아가는 속도
    public float offsetAngle = 0f; // 시작 각도 오프셋

    private void Awake()
    {
        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");

        transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);

    }

    public void Init(int _skillId, int _damage)
    {
        skillInfo = DataManager.Instance.GetSkillTable(_skillId);
        damage = _damage;

        FireDamage[] childs = gameObject.GetComponentsInChildren<FireDamage>();
        //Debug.Log(childs.Length);
        if (childs.Length == 0)
        {
            numberOfObjects = skillInfo.level;
            float projectileAngle = 360f / numberOfObjects;
            float offsetAngle = Random.Range(0f, projectileAngle);

            for (int i = 0; i < numberOfObjects; i++)
            {
                // 원형에 배치될 각도 계산
                float angle = i * projectileAngle + offsetAngle;

                // 각도를 라디안으로 변환하여 위치 계산
                float radian = angle * Mathf.Deg2Rad;
                float x = Mathf.Cos(radian) * orbitRadius;
                float z = Mathf.Sin(radian) * orbitRadius;
                Vector3 position = new Vector3(x, 0.2f, z) + transform.position;
                // 프리팹을 인스턴스화하여 자식으로 만듦
                GameObject obj = Instantiate(rotateObjectPrefab, position, Quaternion.Euler(-90f, 0f, 0f), transform);
                obj.GetComponent<SphereCollider>().isTrigger = true;
                obj.GetComponent<FireDamage>().Init(damage);
                obj.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(LateCall());
    }

    void Update()
    {
        transform.Rotate(rotateVector * Time.deltaTime);
    }

    private IEnumerator LateCall()
    {
        yield return new WaitForSeconds(disableAfterTime);
        if (skillInfo.coolDownTime != 0)
        {
            gameObject.SetActive(false);
        }
        yield break;
    }

}
