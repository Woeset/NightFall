using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System;

public class SoundManager : MonoBehaviour {

    public SoundPack[] sounds;

    void Awake() {
        for (int i = 0; i < sounds.Length; i++) {
            sounds[i].InitSource(gameObject.AddComponent<AudioSource>());
        }
    }

    void Start() {
        
    }

    void Update() {

    }

    public AudioSource GetAudioWithName(string soundName) {
        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].soundName.Equals(soundName)) {
                return sounds[i].GetSource();
            }
        }
        return null;
    }

    public void PlaySound(string soundName) {
        AudioSource audio = GetAudioWithName(soundName);
        if (audio != null) {
            audio.PlayDelayed(0);
        } else {
            print("Audio '" + soundName + "' not found");
        }
    }

    public void PlaySoundWithDelay(string soundName, float delay) {
        AudioSource audio = GetAudioWithName(soundName);
        if (audio != null) {
            audio.PlayDelayed(delay);
        } else {
            print("Audio '" + soundName + "' not found");
        }
    }

    public void StopSound(string soundName) {
        AudioSource audio = GetAudioWithName(soundName);
        if (audio != null) {
            audio.Stop();
        } else {
            print("Audio '" + soundName + "' not found");
        }
    }

    public void PauseSound(string soundName) {
        AudioSource audio = GetAudioWithName(soundName);
        if (audio != null) {
            audio.Pause();
        } else {
            print("Audio '" + soundName + "' not found");
        }
    }

    public void UnPauseSound(string soundName) {
        AudioSource audio = GetAudioWithName(soundName);
        if (audio != null) {
            audio.UnPause();
        } else {
            print("Audio '" + soundName + "' not found");
        }
    }

}

[System.Serializable]
public class SoundPack {

    public string soundName;
    public AudioClip audioClip;
    public AudioMixerGroup output;
    public bool mute = false;
    public bool bypassEffects = false;
    public bool bypassListenerEffects = false;
    public bool bypassReverbZones = false;
    public bool playOnAwake = true;
    public bool loop = false;
    [Range(0, 256)]
    public int priority = 128;
    [Range(0.0F, 1.0F)]
    public float volume = 1.0F;
    [Range(-3.0F, 3.0F)]
    public float pitch = 1.0F;
    [Range(-1.0F, 1.0F)]
    public float stereoPan = 0.0F;
    [Range(0.0F, 1.0F)]
    public float spatialBlend = 0.0F;
    [Range(0.0F, 1.1F)]
    public float reverbZoneMix = 1.0F;

    private AudioSource source;

    public void InitSource(AudioSource audioSource) {
        source = audioSource;

        source.clip = audioClip;
        source.outputAudioMixerGroup = output;
        source.mute = mute;
        source.bypassEffects = bypassEffects;
        source.bypassListenerEffects = bypassListenerEffects;
        source.bypassReverbZones = bypassReverbZones;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.priority = priority;
        source.volume = volume;
        source.pitch = pitch;
        source.panStereo = stereoPan;
        source.spatialBlend = spatialBlend;
        source.reverbZoneMix = reverbZoneMix;
    }

    public AudioSource GetSource() {
        return source;
    }

}