using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.Audio;
public class Audiomanager : MonoBehaviour
{
    
    public AudioMixer audiomixer;
    public float volume = 0.03f;
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {   
        
        foreach(Sound s in sounds){
            bool result =  audiomixer.GetFloat("FxVolume", out volume);
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = volume;
            s.source.pitch = s.pitch;

        }

    }

    // Update is called once per frame

    public void Play(string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }
}
