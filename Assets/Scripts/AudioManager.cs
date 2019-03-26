using UnityEngine;
using System.Collections.Generic;

public class AudioManager  {
    private static AudioManager instance;
    public static AudioManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance;
        }
    }


    private Dictionary<string ,AudioClip> clips;
    private List<AudioSource> sources ;
    private GameObject comObj;
    private AudioManager()
    {
        clips = new Dictionary<string, AudioClip>(20);
        sources = new List<AudioSource>();
    }

    public void StartUp(GameObject go)
    {
        comObj = go;
    }
    private AudioClip GetAudioClip(string audioName)
    {
        if (clips.ContainsKey(audioName))
        {
            return clips[audioName];
        }
        AudioClip clip = Resources.Load<AudioClip>("Audios/" + audioName);
        clips.Add(audioName, clip);
        return clip;
    }

    private AudioSource GetAudioSource()
    {
        for (int i = 0; i < sources .Count; i++)
        {
            if (!sources[i].isPlaying)
            {
                return sources[i];
            }
        }
        var source = comObj .AddComponent<AudioSource>();
        source.playOnAwake = false;
        sources.Add(source);
        return source;
    }

    public float PlayAudio(string audioName, float volume, bool isLoop)
    {
        var s = GetAudioSource();
        s.clip = GetAudioClip(audioName);
        s.volume = volume;
        s.loop = isLoop;
        s.Play();
        return s.clip.length;
    }
    public float PlayAudio(string audioName)
    {
        return PlayAudio(audioName, 1, false);
    }

    private AudioSource hitSource;
    public void PlayHitEnemy()
    {
        if (hitSource == null)
        {
            hitSource = comObj.AddComponent<AudioSource>();
            hitSource.playOnAwake = false;
        }
        if (!hitSource.isPlaying)
        {
            hitSource.clip = GetAudioClip("HitEnemy");
            hitSource.volume = 0.5f;
            hitSource.loop = false;
            hitSource.Play();
        }
    }
}
