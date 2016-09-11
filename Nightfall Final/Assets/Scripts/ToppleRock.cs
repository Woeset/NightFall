using UnityEngine;
using System.Collections;

public class ToppleRock : MonoBehaviour {

    public GameObject rock;

    void Start() {

    }

    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            rock.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

}