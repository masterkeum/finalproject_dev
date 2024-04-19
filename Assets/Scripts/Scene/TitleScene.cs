using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public GameObject tapToStart;
    public GameObject loginButtons;

    private void Awake()
    {
        _ = DataManager.Instance;
        loginButtons.SetActive(false);
        SoundManager.Instance.CreateSFXAudioSource();
        SoundManager.Instance.ChangeBackGroundMusic(Resources.Load<AudioClip>("Audio/Music/a_small_fire_will_do"), SoundManager.Instance.musicAudioSource.volume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 뒤로 가기 버튼
            UI2Btn escapePopup = UIManager.Instance.ShowUI<UI2Btn>();
            escapePopup.SetPopup("게임을 종료 하시겠습니까?", () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
            });
        }
    }

    public void TapToStart()
    {
        tapToStart.SetActive(false);
        loginButtons.SetActive(true);
        SoundManager.Instance.PlaySound("SelectUI_2");
    }

    public void GuestLogin()
    {
        Debug.Log("GetCommonReward");
        SceneManager.LoadScene("MainScene");
    }
}
