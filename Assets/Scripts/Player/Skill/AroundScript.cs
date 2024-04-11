using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundScript : MonoBehaviour
{
    public Vector3 rotateVector = new Vector3(0, 0, 180f);

    private Transform parentObject;
    private LayerMask enemyLayerMask;

    SkillTable skillInfo;
    private int damage;


    public GameObject rotateObjectPrefab;

    public int numberOfObjects = 1; // 돌고 있는 자식 객체의 개수
    public float orbitRadius = 2f; // 중심으로부터의 거리
    //public float orbitSpeed = 30f; // 돌아가는 속도
    public float offsetAngle = 0f; // 시작 각도 오프셋

    private void Awake()
    {
        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
    }

    public void Init(int _skillId, int _damage)
    {
        skillInfo = DataManager.Instance.GetSkillTable(_skillId);
        damage = _damage;

        transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);

        numberOfObjects = skillInfo.level;
        for (int i = 0; i < numberOfObjects; i++)
        {
            // 원형에 배치될 각도 계산
            float angle = i * (360f / numberOfObjects) + offsetAngle;

            // 각도를 라디안으로 변환하여 위치 계산
            float radian = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(radian) * (orbitRadius + numberOfObjects);
            float z = Mathf.Sin(radian) * (orbitRadius + numberOfObjects);
            Vector3 position = new Vector3(x, 0.2f, z) + transform.position;
            // 프리팹을 인스턴스화하여 자식으로 만듦
            GameObject obj = Instantiate(rotateObjectPrefab, position, Quaternion.identity, transform);
            obj.SetActive(true);
        }
    }

    void Update()
    {
        transform.Rotate(rotateVector * Time.deltaTime);
    }
}
