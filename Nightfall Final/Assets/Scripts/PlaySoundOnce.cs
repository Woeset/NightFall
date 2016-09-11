using UnityEngine;
using System.Collections;

public class PlaySoundOnce : MonoBehaviour {

    public GameManager gameManager;
    public SoundManager soundManager;
    public string soundName;
    public bool onStart = true;

    private AudioSource audioSource;
    private float originalVolume;
    private bool first = true;

    void Start() {
        audioSource = soundManager.GetAudioWithName(soundName);
        if (audioSource != null) {
            originalVolume = audioSource.volume;
        }

        if (onStart) {
            PlaySound();
        }
    }

    void Update() {
        
    }

    public void PlayOnce() {
        if (!onStart && first) {
            PlaySound();
            first = false;
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