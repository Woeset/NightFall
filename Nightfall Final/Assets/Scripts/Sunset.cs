using UnityEngine;
using System.Collections;

public class Sunset : MonoBehaviour {

    public LoadTransition levelComplete;
    public float intensityDecrease = 4.0F;
    public Transform movement;
    public float heightDecrease = 5.0F;
    public float sunsetDuration = 10.0F;
    public Light spotlight;

    private Light sunlight;
    private float intensity;
    private float height;
    private float sunsetTimer;
    private bool isSetting;
    
	void Start () {
        sunlight = gameObject.GetComponent<Light>();
        intensity = sunlight.intensity;
        height = gameObject.transform.position.y;
	}
	
	void Update () {
	    if (isSetting) {
            sunsetTimer += Time.deltaTime;
            float perc = sunsetTimer / sunsetDuration;
            sunlight.intensity = intensity - intensityDecrease * perc;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, movement.position.y + height - heightDecrease * perc, gameObject.transform.position.z);
            if (sunlight.intensity >= 0.6F) {
                spotlight.intensity = 1.0F - (perc - 0.6F);
            }
            if (sunlight.intensity <= 0F) {
                spotlight.intensity = 0.0F;
                sunlight.intensity = 0.0F;
                isSetting = false;
                levelComplete.CompleteLevel();
            }
        }
	}

    public void TriggerSunset() {
        isSetting = true;
    }

}