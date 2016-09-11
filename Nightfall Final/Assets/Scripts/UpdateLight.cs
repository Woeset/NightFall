using UnityEngine;
using System.Collections;

public class UpdateLight : MonoBehaviour {

    public GameManager gameManager;
    public GameObject player;
    public Light[] lights;

    private FlashlightBatteryLife batteryLife;
    private float[] originalIntensity;

	void Start() {
        batteryLife = player.GetComponentInChildren<FlashlightBatteryLife>();

        if (lights != null) {
            originalIntensity = new float[lights.Length];
            for (int i = 0; i < lights.Length; i++) {
                originalIntensity[i] = lights[i].intensity;
            }
        }
    }
	
	void Update() {
        if (batteryLife != null) {
            float life = batteryLife.GetBatteryLife();
            float darkness = gameManager.Darkness;
            for (int i = 0; i < lights.Length; i++) {
                lights[i].intensity = originalIntensity[i] * darkness * life;
            }
        }
	}

}