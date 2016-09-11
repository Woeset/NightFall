using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateSliders : MonoBehaviour {

    public GameManager gameManager;

    private Slider[] sliders;
    private Toggle toggle;
    private static bool permadeath;
    private static float darkness;
    private static float volume;

    void Start() {
        toggle = GameObject.FindObjectOfType<Toggle>();
        sliders = GameObject.FindObjectsOfType<Slider>();
        permadeath = gameManager.Permadeath;
        darkness = gameManager.Darkness;
        volume = gameManager.Volume;
        if (toggle != null) {
            toggle.isOn = permadeath;
        }
        if (sliders != null) {
            for (int i = 0; i < sliders.Length; i++) {
                string name = sliders[i].name;
                if (name.Equals("Darkness")) {
                    sliders[i].value = darkness;
                } else if (name.Equals("Volume")) {
                    sliders[i].value = volume;
                }
            }
        }
    }

    void Update() {
        
    }

    public void OnToggleUpdate() {
        if (toggle != null) {
            gameManager.Permadeath = toggle.isOn;
        }
    }

    public void OnSliderUpdate() {
        if (sliders != null) {
            for (int i = 0; i < sliders.Length; i++) {
                string name = sliders[i].name;
                if (name.Equals("Darkness")) {
                    gameManager.Darkness = sliders[i].value;
                } else if (name.Equals("Volume")) {
                    gameManager.Volume = sliders[i].value;
                }
            }
        }
    }

}