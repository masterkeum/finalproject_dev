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

    private void Start()
    {
        loadProgress.value = 0;
        loadProgress.maxValue = progressMaxValue;
        loadMsg.text = "[Game Tip!] Test Message";
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
                loadMsg.text = "[Load Complete!] Press the Mouse L-Button";
                yield break;
            }
            yield return null;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
