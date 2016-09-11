using UnityEngine;
using System.Collections;

public class BoulderWall : MonoBehaviour {

    public Rigidbody2D boulder;
    public BoxCollider2D on;
    public BoxCollider2D off;

    void Start() {
	    
	}
	
	void Update() {
	    
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Boulder")) {
            boulder.isKinematic = true;
            on.enabled = false;
            off.enabled = true;
        }
    }

}