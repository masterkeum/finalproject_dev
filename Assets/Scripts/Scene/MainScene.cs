using UnityEngine;

public class MainScene : MonoBehaviour
{
    private OutGameHUD outGameHUD;

    private void Awake()
    {
        Debug.Log("MainScene.Awake");
        Time.timeScale = 1.0f;
        _ = DataManager.Instance;
        SoundManager.Instance.CreateSFXAudioSource();
        SoundManager.Instance.ChangeBackGroundMusic(Resources.Load<AudioClip>("Audio/Music/wednesday_night"), SoundManager.Instance.musicAudioSource.volume);
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
