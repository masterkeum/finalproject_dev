using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    TMP_Text text;
    public Color color;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponent<TMP_Text>();
        //color = text.color;
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
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