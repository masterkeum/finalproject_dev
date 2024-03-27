using UnityEngine;

public class MainScene : MonoBehaviour
{
    private OutGameHUD outGameHUD;
    
    private void Awake()
    {
        Time.timeScale = 1.0f;
        _ = DataManager.Instance;
        _ = GameManager.Instance;
        UIManager.Instance.Clear();
    }

    private void Start()
    {
        Debug.Log("MainScene.Start");
        GameManager.Instance.MainSceneProcess();

        outGameHUD = UIManager.Instance.ShowUI<OutGameHUD>();
    }
}
