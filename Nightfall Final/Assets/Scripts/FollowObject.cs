using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {
    
    public Transform obj;

	void Start() {
	    
	}
	
	void Update() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(obj.transform.position);
        gameObject.transform.position = new Vector3(pos.x, pos.y, gameObject.transform.position.z);
	}

}