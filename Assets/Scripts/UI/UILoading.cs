using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : UIBase
{
    [SerializeField] private Slider loadProgress;
    [SerializeField] private TextMeshProUGUI loadMsg;
    private float progressMaxValue = 5f;
    public bool IsReady { get; private set; }

    private void Start()
    {
        IsReady = false;
        loadProgress.value = 0;
        loadProgress.maxValue = progressMaxValue;
        loadMsg.text = "[ 데이터를 로딩중입니다! ]";
        StartCoroutine(Loading());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && loadProgress.value >= progressMaxValue)
        {
            LoadNextScene();
        }
    }


    IEnumerator Loading()
    {
        while (true)
        {
            loadProgress.value += Time.deltaTime;
            if (loadProgress.value >= progressMaxValue)
            {
                loadMsg.text = "[ 로딩완료. 터치하세요! ]";
                IsReady = true;
                yield break;
            }
            yield return null;
        }
    }

    private void LoadNextScene()
    {
        //SceneManager.LoadScene(1); // 로딩씬
        SceneManager.LoadScene(2); // 메인씬
    }

}
