using UnityEngine;

public class MainScene : MonoBehaviour
{
    private void Awake()
    {
        _ = DataManager.Instance;
        _ = GameManager.Instance;
        UIManager.Instance.Clear();
    }

}
