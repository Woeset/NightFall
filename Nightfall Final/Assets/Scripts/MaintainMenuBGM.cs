using UnityEngine;
using System.Collections;

public class MaintainMenuBGM : MonoBehaviour {

    public GameManager gameManager;
    public SoundManager soundManager;
    public string soundName;

    private AudioSource audioSource;
    private float originalVolume;
    private float timeIn;

    void Start () {
        audioSource = soundManager.GetAudioWithName(soundName);
        if (audioSource != null) {
            originalVolume = audioSource.volume;
        }
        timeIn = gameManager.TimeIn;

    }
	
	void Update () {
        if (audioSource != null) {
            if (!audioSource.isPlaying) {
                audioSource.time = timeIn;
                audioSource.volume = originalVolume * gameManager.Volume;
                audioSource.PlayDelayed(0);
            }
            audioSource.volume = originalVolume * gameManager.Volume;
        } else {
            print("Audio '" + soundName + "' not found");
        }
    }

    public void OnChangeMenu() {
        gameManager.TimeIn = audioSource.time;
    }

}