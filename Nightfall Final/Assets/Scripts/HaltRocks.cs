using UnityEngine;
using System.Collections;

public class HaltRocks : MonoBehaviour {
    
	void Start() {
	    
	}
	
	void Update() {
	    
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Rock")) {
            collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

}