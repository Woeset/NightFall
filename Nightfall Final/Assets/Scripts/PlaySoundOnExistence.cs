using UnityEngine;
using System.Collections;

public class PlaySoundOnExistence : MonoBehaviour {

    public GameManager gameManager;
    public SoundManager soundManager;
    public string soundName;

    private AudioSource audioSource;
    private float originalVolume;
    private float fadeTimer;
    private bool isFading;

	void Start() {
        audioSource = soundManager.GetAudioWithName(soundName);
        if (audioSource != null) {
            originalVolume = audioSource.volume;
        }
    }
	
	void Update() {
        if (!isFading) {
            if (audioSource != null) {
                if (!audioSource.isPlaying) {
                    audioSource.volume = originalVolume * gameManager.Volume;
                    audioSource.PlayDelayed(0);
                }
            } else {
                print("Audio '" + soundName + "' not found");
            }
        } else {
            fadeTimer -= Time.deltaTime;
            if (fadeTimer <= 0.0F) {
                fadeTimer = 0.0F;
                StopSound();
            }
            audioSource.volume = originalVolume * gameManager.Volume * fadeTimer;
        }
    }

    public void FadeOutSound() {
        if (!isFading && audioSource != null && audioSource.isPlaying) {
            isFading = true;
            fadeTimer = 1.0F;
        }
    }

    public void StopSound() {
        if (audioSource != null) {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }
        } else {
            print("Audio '" + soundName + "' not found");
        }
    }

}