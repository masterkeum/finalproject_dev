using UnityEngine;

public class SoundManager : SingletoneBase<SoundManager>
{
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    Pooling objectPool;
    AudioSource musicAudioSource;
    public AudioClip clip;


    [SerializeField] GameObject curObj;

    protected override void Init()
    {
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.clip = clip;
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
        musicAudioSource.Play();

        objectPool = GetComponent<Pooling>();
        objectPool.CreatePool(transform);
    }


    public void ChangeBackGroundMusic(AudioClip clip, float soundVolume)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.volume = soundVolume;
        musicAudioSource.Play();

    }
    //public void PlayClip(AudioClip clip, float volume)
    //{
    //    curObj = objectPool.GetPoolItem("SoundSource");
    //    curObj.SetActive(true);
    //    SoundSource soundSource = curObj.GetComponent<SoundSource>();
    //    soundSource.Play(clip, volume);
    //}
    //public void PlayClip(AudioClip clip)
    //{
    //    curObj = objectPool.GetPoolItem("SoundSource");
    //    curObj.SetActive(true);
    //    SoundSource soundSource = curObj.GetComponent<SoundSource>();
    //    soundSource.Play(clip, soundEffectVolume);
    //}

    public GameObject CurSoundSource()
    {
        return curObj;
    }

    public void StopClip()
    {
        curObj.GetComponent<SoundSource>().Disable();
    }

}
