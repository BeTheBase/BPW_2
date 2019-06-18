using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] _Sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in _Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.spatialBlend = s.SpacialSound;
        }
    }

    private void Start()
    {
        //Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(_Sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound:" + name + "is not found!");
            return;
        }
        s.Source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(_Sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound:" + name + "is not found!");
            return;
        }
        s.Source.Stop();
    }
}
