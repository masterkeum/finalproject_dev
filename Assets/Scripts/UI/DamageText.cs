using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float moveSpeed = 2.0f;
    private float alphaSpeed = 2.0f;
    private float destroyTime = 2.0f;
    TMP_Text text;
    public Color color;
    public int damage;

    void Awake()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;
        text = GetComponent<TMP_Text>();

        Invoke("DestroyObject", destroyTime);
    }

    public void Init(int _damage, Color _color)
    {
        damage = _damage;
        color = _color;
        text.text = damage.ToString();
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = color;
    }

    private void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}