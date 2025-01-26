using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Death,
    Shake,
    Scream,
    Canon,
    MainMusic,
    HappyScream,
    MECHE,
    BOOST,
    REFILL,
    MUSIC2
}


[Serializable]
public class Sound
{
    public SoundType name;
    public List<AudioSource> clips;
}

public class SoundManager : MonoBehaviour
{
    public List<Sound> sounds;
    public static SoundManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(SoundType soundType)
    {
        Sound sound = sounds.Find(s => s.name == soundType);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundType + " not found!");
            return;
        }
        sound.clips[UnityEngine.Random.Range(0, sound.clips.Count)].Play();
    }
    
    public void StopSound(SoundType soundType)
    {
        Sound sound = sounds.Find(s => s.name == soundType);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundType + " not found!");
            return;
        }
        foreach (var clip in sound.clips)
        {
            clip.Stop();
        }
    }
}
