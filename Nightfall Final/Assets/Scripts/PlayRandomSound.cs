using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayRandomSound : MonoBehaviour {

    public GameManager gameManager;
    public SoundManager soundManager;
    public string soundName;
    public float minDelay = 5.0F;
    public float frequency = 0.1F;
    public float procChance = 0.02F;

    private AudioSource audioSource;
    private float originalVolume;
    private float timer;

    void Start() {
        audioSource = soundManager.GetAudioWithName(soundName);
        if (audioSource != null) {
            originalVolume = audioSource.volume;
        }
        timer = -minDelay;
    }
	
	void Update() {
        timer += Time.deltaTime;
        if (timer >= frequency) {
            timer = 0.0F;
            if (UnityEngine.Random.Range(1, (int) (1 / procChance)) == 1) {
                PlaySound();
                timer = -minDelay;
            }
        }
    }

    void PlaySound() {
        if (audioSource != null) {
            if (!audioSource.isPlaying) {
                audioSource.volume = originalVolume * gameManager.Volume;
                audioSource.PlayDelayed(0);
            }
        } else {
            print("Audio '" + soundName + "' not found");
        }
    }

}