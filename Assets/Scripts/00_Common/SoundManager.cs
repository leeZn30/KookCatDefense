using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    Main,
    Map1,
    Map2,
}
public enum GameSFX
{

    CatHappy,
    CatAttack,
    Bullet,
    Catnip,
    Razer,
    SkillCount,
    Pang

}
public enum SFX
{   
    ButtonClick,
    CatSoundClick,
    GameOver,
    GameClear,
}
public class SoundManager : Singleton<SoundManager>

{ 
    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    public AudioSource gameSFXAudioSource;


    
    public List<AudioClip> bgmAudioClip;
    public List<AudioClip> sfxAudioClips;
    public List<AudioClip> gameSFXAudioClips;

    public float BGMVolume
    {
        set => bgmAudioSource.volume =  value;
        get => bgmAudioSource.volume;
    }
    public float SFXVolume
    {
        set => sfxAudioSource.volume = value;
        get => sfxAudioSource.volume;
    }
    public float GameSFXVolume
    {
        set => gameSFXAudioSource.volume = value;
        get => gameSFXAudioSource.volume;
    }
    // Start is called before the first frame update
    void Start()
    {
        

        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        bgmAudioSource.loop = true;
        sfxAudioSource.loop = false;
        
    }
    public void PlayGameSFX(GameSFX sfx, float time = 0.0f)
    {
        if (gameSFXAudioClips[(int)sfx] != null)
        {
            StartCoroutine(PlayGameSFX(gameSFXAudioClips[(int)sfx], time));
        }

    }

    private IEnumerator PlayGameSFX(AudioClip audio, float time)
    {
        yield return new WaitForSeconds(time);
        gameSFXAudioSource.PlayOneShot(audio);
    }

    public void PlaySFX(SFX sfx, float time = 0.0f)
    {
        if (sfxAudioClips[(int)sfx] != null)
        {
            StartCoroutine(PlaySFX(sfxAudioClips[(int)sfx], time));
        }
        
    }

    private IEnumerator PlaySFX(AudioClip audio, float time)
    {
        yield return new WaitForSeconds(time);
        sfxAudioSource.PlayOneShot(audio);
    }
    public void PlayBGM(BGM bgm=BGM.Main)
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = bgmAudioClip[(int)bgm];
        bgmAudioSource.Play();
    }


    public void SetMasterVolume(float soundValue = 1.0f)
    {

        AudioListener.volume = soundValue;

    }


}
