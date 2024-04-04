using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : SingletoneBase<SoundManager>
{
    [Range(0f, 1f)] public float soundEffectVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public AudioSource battleAudioSource;
    private Dictionary<string, AudioClip> uiSFXDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> battleDict = new Dictionary<string, AudioClip>();

    [SerializeField] GameObject curObj;

    protected override void Init()
    {
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource.loop = true;

        LoadUISoundClip();
        LoadBattleSoundclip();
        ChangeBackGroundMusic(Resources.Load<AudioClip>("Audio/Music/wednesday_night"), musicAudioSource.volume);
    }

    public void CreateSFXAudioSource()
    {
        GameObject uiSfxSource = new GameObject("uiSfxSource");
        sfxAudioSource = uiSfxSource.AddComponent<AudioSource>();
    }

    private void LoadUISoundClip()
    {
        AudioClip[] uiClips = Resources.LoadAll<AudioClip>("Audio/UI");
        foreach (AudioClip clip in uiClips)
        {
            uiSFXDict.Add(clip.name, clip);
        }
    }

    private void LoadBattleSoundclip()
    {
        AudioClip[] uiClips = Resources.LoadAll<AudioClip>("Audio/Battle");
        foreach (AudioClip clip in uiClips)
        {
            battleDict.Add(clip.name, clip);
        }
    }

    public void ChangeBackGroundMusic(AudioClip clip, float volume)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.volume = volume;
        musicAudioSource.Play();
    }

    public void StopBackGroundMusic()
    {
        musicAudioSource.Stop();
    }

    public void PlaySound(string clipName, float volume = 1.0f)
    {
        if (uiSFXDict.ContainsKey(clipName) == false)
        {
            Debug.Log(clipName + "클립이 없습니다.");
            return;
        }
        sfxAudioSource.PlayOneShot(uiSFXDict[clipName], volume * soundEffectVolume);
    }

    public void PlayBattleSound(string clipName, float volume = 1.0f)
    {
        if (battleDict.ContainsKey(clipName) == false)
        {
            Debug.Log(clipName + "클립이 없습니다.");
            return;
        }
        battleAudioSource.PlayOneShot(battleDict[clipName], volume * soundEffectVolume);
    }



}
