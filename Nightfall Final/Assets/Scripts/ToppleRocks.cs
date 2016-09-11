using UnityEngine;
using System.Collections;

public class ToppleRocks : MonoBehaviour {

    public GameObject[] rocks;

    void Start() {
	    
	}
	
	void Update() {
	    
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Boulder")) {
            for (int i = 0; i < rocks.Length; i++) {
                rocks[i].GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }

}