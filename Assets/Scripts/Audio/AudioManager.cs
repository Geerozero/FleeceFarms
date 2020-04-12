using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public soundMasterController[] sounds;

    void Awake()
    {
        foreach (soundMasterController s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        soundMasterController s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
