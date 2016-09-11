using UnityEngine;
using System.Collections;

public class WeighInWater : MonoBehaviour {

    private float timer = -1;
    
	void Start() {
	    
	}
	
	void Update() {
	    if (timer >= 0.0F) {
            timer += Time.deltaTime;
            if (timer >= 0.25F) {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Water")) {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 100;
            timer = 0.0F;
        }
    }

}