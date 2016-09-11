using UnityEngine;
using System.Collections;

public class DestroyAndPlaySound : MonoBehaviour {

    public SoundManager soundManager;
    public string soundName;

    private AudioSource audioSource;

    void Start() {
        audioSource = soundManager.GetAudioWithName(soundName);
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            if (gameObject.GetComponent<PlaySoundOnExistence>() != null) {
                gameObject.GetComponent<PlaySoundOnExistence>().FadeOutSound();
            }
            if (audioSource != null) {
                if (!audioSource.isPlaying) {
                    audioSource.PlayDelayed(0);
                }
            } else {
                print("Audio '" + soundName + "' not found");
            }
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}