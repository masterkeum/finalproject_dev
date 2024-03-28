using UnityEngine;

public class MainScene : MonoBehaviour
{
    private OutGameHUD outGameHUD;

    private void Awake()
    {
        Debug.Log("MainScene.Awake");
        Time.timeScale = 1.0f;
        _ = DataManager.Instance;
        GameManager.Instance.Clear();
        UIManager.Instance.Clear();
    }

    private void Start()
    {
        Debug.Log("MainScene.Start");
        GameManager.Instance.MainSceneProcess();

        outGameHUD = UIManager.Instance.ShowUI<OutGameHUD>();
    }
}
