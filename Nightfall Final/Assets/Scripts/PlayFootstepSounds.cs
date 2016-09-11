using UnityEngine;
using System.Collections;
using Prime31;

public class PlayFootstepSounds : MonoBehaviour {

    public GameManager gameManager;
    public SoundManager soundManager;
    public string grassSoundName;
    public string mudSoundName;
    public string rockSoundName;
    public string woodSoundName;

    private AudioSource grassAudioSource;
    private AudioSource mudAudioSource;
    private AudioSource rockAudioSource;
    private AudioSource woodAudioSource;
    private CharacterController2D controller;
    private float grassVolume;
    private float mudVolume;
    private float rockVolume;
    private float woodVolume;

    void Start() {
        grassAudioSource = soundManager.GetAudioWithName(grassSoundName);
        mudAudioSource = soundManager.GetAudioWithName(mudSoundName);
        rockAudioSource = soundManager.GetAudioWithName(rockSoundName);
        woodAudioSource = soundManager.GetAudioWithName(woodSoundName);

        controller = gameObject.GetComponent<CharacterController2D>();

        if (grassAudioSource != null) {
            grassVolume = grassAudioSource.volume;
        }
        if (mudAudioSource != null) {
            mudVolume = mudAudioSource.volume;
        }
        if (rockAudioSource != null) {
            rockVolume = rockAudioSource.volume;
        }
        if (woodAudioSource != null) {
            woodVolume = woodAudioSource.volume;
        }
    }

    void Update() {
        Collider2D ground = controller.ground;
	    if (controller.isGrounded && ground != null) {
            if (ground.tag.Equals("Grass")) {
                if (grassAudioSource != null && !grassAudioSource.isPlaying) {
                    grassAudioSource.volume = grassVolume * gameManager.Volume;
                    grassAudioSource.PlayDelayed(0);
                }
                if (mudAudioSource != null && mudAudioSource.isPlaying) {
                    mudAudioSource.Stop();
                }
                if (rockAudioSource != null && rockAudioSource.isPlaying) {
                    rockAudioSource.Stop();
                }
                if (woodAudioSource != null && woodAudioSource.isPlaying) {
                    woodAudioSource.Stop();
                }
            } else if (ground.tag.Equals("Mud")) {
                if (grassAudioSource != null && grassAudioSource.isPlaying) {
                    grassAudioSource.Stop();
                }
                if (mudAudioSource != null && !mudAudioSource.isPlaying) {
                    mudAudioSource.volume = mudVolume * gameManager.Volume;
                    mudAudioSource.PlayDelayed(0);
                }
                if (rockAudioSource != null && rockAudioSource.isPlaying) {
                    rockAudioSource.Stop();
                }
                if (woodAudioSource != null && woodAudioSource.isPlaying) {
                    woodAudioSource.Stop();
                }
            } else if (ground.tag.Equals("Rock")) {
                if (grassAudioSource != null && grassAudioSource.isPlaying) {
                    grassAudioSource.Stop();
                }
                if (mudAudioSource != null && mudAudioSource.isPlaying) {
                    mudAudioSource.Stop();
                }
                if (rockAudioSource != null && !rockAudioSource.isPlaying) {
                    rockAudioSource.volume = rockVolume * gameManager.Volume;
                    rockAudioSource.PlayDelayed(0);
                }
                if (woodAudioSource != null && woodAudioSource.isPlaying) {
                    woodAudioSource.Stop();
                }
            } else if (ground.tag.Equals("Wood")) {
                if (grassAudioSource != null && grassAudioSource.isPlaying) {
                    grassAudioSource.Stop();
                }
                if (mudAudioSource != null && mudAudioSource.isPlaying) {
                    mudAudioSource.Stop();
                }
                if (rockAudioSource != null && !rockAudioSource.isPlaying) {
                    rockAudioSource.Stop();
                }
                if (woodAudioSource != null && woodAudioSource.isPlaying) {
                    woodAudioSource.volume = woodVolume * gameManager.Volume;
                    woodAudioSource.PlayDelayed(0);
                }
            }
        }
	}

}