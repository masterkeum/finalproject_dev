using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public GameObject tapToStart;
    public GameObject loginButtons;
    private void Awake()
    {
        loginButtons.SetActive(false);
        SoundManager.Instance.CreateSFXAudioSource();
        SoundManager.Instance.ChangeBackGroundMusic(Resources.Load<AudioClip>("Audio/Music/a_small_fire_will_do"), SoundManager.Instance.musicAudioSource.volume);
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
