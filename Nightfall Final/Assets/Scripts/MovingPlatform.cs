using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Vector3 endPosition = Vector3.zero;
    public float speed = 1.0F;
    public float delay = -2.0F;

    private float timer = 0.0F;
    private Vector3 startPosition = Vector3.zero;
    private bool outgoing = true;

    // Use this for initialization
    void Start () {
        startPosition = gameObject.transform.position;
        endPosition += startPosition;

        float dist = Vector3.Distance(startPosition, endPosition);
        if (dist != 0) {
            speed /= dist;
        }

        timer = delay;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * speed;

        if (timer >= 0.0F) {
            if (outgoing) {
                gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
                if (timer > 1.0F) {
                    outgoing = false;
                    timer = delay;
                }
            } else {
                gameObject.transform.position = Vector3.Lerp(endPosition, startPosition, timer);
                if (timer > 1.0F) {
                    outgoing = true;
                    timer = delay;
                }
            }
        }
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + endPosition);
    }

}