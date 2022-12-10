using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public List<AudioSource> channels;
    private List<AudioClip> audioClips;

    private void Awake()
    {
        var obj = FindObjectsOfType<SoundManager>();
        if (obj.Length == 1)
        {
            InitializeSoundFX();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }  

    private void InitializeSoundFX()
    {
        channels = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();
        audioClips.Add(Resources.Load<AudioClip>("Audio/stepstone_1"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/TownTheme"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Battle Line v1_0"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Attack"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Heal"));
    }

    public void PlaySoundFX(Sound sound, Chanel channel)
    {
        channels[(int)channel].clip = audioClips[(int)sound];
        channels[(int)channel].loop = false;
        channels[(int)channel].Play();
    }

    public void PlayMusic(Sound sound)
    {
        channels[(int)Chanel.MUSIC].clip = audioClips[(int)sound];
        channels[(int)Chanel.MUSIC].volume = 0.25f;
        channels[(int)Chanel.MUSIC].loop = true;
        channels[(int)Chanel.MUSIC].Play();
    }

    public void StopMusic()
    {
        channels[(int)Chanel.MUSIC].Stop();
    }
}
