using UnityEngine;
using System.Collections;

public class FlipLog : MonoBehaviour {

    public GameObject player;
    public Rigidbody2D log;

	void Start() {
	    
	}
	
	void Update() {
	    
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            log.AddForceAtPosition(new Vector2(0, 250F), new Vector2(player.transform.position.x, log.gameObject.transform.position.y));
        }
    }

}