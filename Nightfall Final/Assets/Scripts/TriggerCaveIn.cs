using UnityEngine;
using System.Collections;

public class TriggerCaveIn : MonoBehaviour {

    public GameObject caveInWall;
    public GameObject caveInBlockade;

    void Start() {
	    
	}
	
	void Update() {
	    
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            if (caveInWall != null) {
                caveInWall.SetActive(false);
            }
            if (caveInBlockade != null) {
                caveInBlockade.SetActive(true);
            }
        }
    }

}