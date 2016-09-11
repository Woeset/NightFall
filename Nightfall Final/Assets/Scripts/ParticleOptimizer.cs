using UnityEngine;
using System.Collections;

public class ParticleOptimizer : MonoBehaviour {

    public ParticleSystem[] particleSystems;
    public GameObject player;
    public float distanceToActivate = 50F;
    public bool particlesOn = true;

	void Start() {
	    
	}
	
	void Update() {
        if (particlesOn) {
            for (int i = 0; i < particleSystems.Length; i++) {
                float dist = Vector2.Distance(player.transform.position, particleSystems[i].transform.position);
                if (dist <= distanceToActivate) {
                    ParticleSystem.EmissionModule emmission = particleSystems[i].emission;
                    emmission.enabled = true;
                    particleSystems[i].gameObject.SetActive(true);
                } else {
                    ParticleSystem.EmissionModule emmission = particleSystems[i].emission;
                    emmission.enabled = false;
                    particleSystems[i].gameObject.SetActive(false);
                }
            }
        } else {
            for (int i = 0; i < particleSystems.Length; i++) {
                particleSystems[i].gameObject.SetActive(false);
            }
        }
	}

}