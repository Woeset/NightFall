using UnityEngine;
using System.Collections;

public class ParticleOptimization : MonoBehaviour {

    public GameObject[] particleSystems;
    public float distance = 25.0F;

    private int currSystem;

	void Start () {
        currSystem = 0;
    }
	
	void Update () {
        float dist = Vector3.Distance(particleSystems[currSystem].transform.position, gameObject.transform.position);
	    if (dist >= distance) {
            Vector3 displacement = new Vector3(0.75F * (gameObject.transform.position.x - particleSystems[currSystem].transform.position.x), 0.75F * (gameObject.transform.position.y - particleSystems[currSystem].transform.position.y), 0);
            NextSystem();
            particleSystems[currSystem].transform.position = gameObject.transform.position + displacement;
        }
	}

    void NextSystem() {
        if (currSystem + 1 < particleSystems.Length) {
            currSystem++;
        } else {
            currSystem = 0;
        }
    }

}