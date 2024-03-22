using UnityEngine;

public class MainScene : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
        _ = DataManager.Instance;
        _ = GameManager.Instance;
        UIManager.Instance.Clear();
    }

}
