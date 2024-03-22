using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
        _ = DataManager.Instance;
        UIManager.Instance.Clear();
    }

    private void Start()
    {
        UIManager.Instance.ShowUI<UILoading>();
    }
}
