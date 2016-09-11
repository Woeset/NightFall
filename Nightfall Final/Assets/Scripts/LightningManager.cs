using UnityEngine;
using System.Collections;

public class LightningManager : MonoBehaviour {

    public LightningBolt lightning;
    public bool enabler;

    void Start() {
	    
	}
	
	void Update() {
	    
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            if (lightning != null) {
                lightning.enabled = enabler;
            }
        }
    }

}