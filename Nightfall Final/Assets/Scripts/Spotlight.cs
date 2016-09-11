using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour {

    public GameManager gameManager;
    public Light spotlight;
    public float updateRate = 0.025F;
    public float varyingBrightness = 0.05F;
    public bool movable = true;

    private float originalIntensity;
    private float percentBrightness;
    private float brightnessTimer;

    void Start() {
        originalIntensity = spotlight.intensity;
        percentBrightness = 1.0F;
        brightnessTimer = 0.0F;
    }
	
	void Update() {
        brightnessTimer += Time.deltaTime;

        if (brightnessTimer >= updateRate) {
            brightnessTimer = 0.0F;
            percentBrightness = UnityEngine.Random.Range(percentBrightness - varyingBrightness, percentBrightness + varyingBrightness);

            if (percentBrightness < 0.5F) {
                percentBrightness = 0.5F;
            } else if (percentBrightness > 1.5F) {
                percentBrightness = 1.5F;
            }
        }

        spotlight.intensity = originalIntensity * gameManager.Darkness * percentBrightness;
        if (movable) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(mousePosition.x, mousePosition.y, -0.5F);
        }
    }

}