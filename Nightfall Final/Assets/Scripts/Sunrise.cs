using UnityEngine;
using System.Collections;

public class Sunrise : MonoBehaviour {

    public LoadTransition levelComplete;
    public float intensityIncrease = 4.0F;
    public Transform movement;
    public float heightIncrease = 5.0F;
    public float sunriseDuration = 10.0F;

    private Light sunlight;
    private float intensity;
    private float height;
    private float sunriseTimer;
    private bool isRising;

    void Start() {
        sunlight = gameObject.GetComponent<Light>();
        intensity = sunlight.intensity;
        height = gameObject.transform.position.y;
        TriggerSunrise();
    }

    void Update() {
        if (isRising) {
            sunriseTimer += Time.deltaTime;
            float perc = sunriseTimer / sunriseDuration;
            sunlight.intensity = intensity + intensityIncrease * perc;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, movement.position.y + height + heightIncrease * perc, gameObject.transform.position.z);
            print(sunriseTimer + " " + sunriseDuration);
            if (sunriseTimer >= sunriseDuration) {
                isRising = false;
                levelComplete.CompleteLevel();
            }
        }
    }

    public void TriggerSunrise() {
        isRising = true;
    }

}