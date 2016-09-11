using UnityEngine;
using System.Collections;

public class PermadeathUpdate : MonoBehaviour {

    public GameManager gameManager;
    public Light pointLight;

    private float originalIntensity;

	void Start() {
        originalIntensity = pointLight.intensity;
    }
	
	void Update() {
        pointLight.intensity = gameManager.Permadeath ? originalIntensity : 0.0F;
	}

}