using UnityEngine;
using System.Collections;

public class ButterflyAI : MonoBehaviour {

    public Vector3[] path = new Vector3[] { (new Vector3(0, 0, 0)) };
    public float speed = 1.0F;

    private Vector3[] butterflyPath;
    private float timer;
    private int pathPoint;

    void Start() {
        pathPoint = 0;
        timer = 0.0F;
        speed = 1.0F;

        Vector3 lastPos = gameObject.transform.position;
        butterflyPath = new Vector3[path.Length + 1];
        for (int i = 0; i < path.Length; i++) {
            lastPos += path[i];
            butterflyPath[i] = lastPos;
        }
        butterflyPath[butterflyPath.Length - 1] = gameObject.transform.position;
    }

    void Update() {
        FixFacing();
        MoveFirefly();
    }

    void FixFacing() {
        Vector3 startPosition = butterflyPath[pathPoint];
        Vector3 endPosition = (pathPoint + 1 < butterflyPath.Length) ? butterflyPath[pathPoint + 1] : butterflyPath[0];
        Vector3 angle = gameObject.transform.localEulerAngles;
        Vector3 scale = gameObject.transform.localScale;
        if (endPosition.x - startPosition.x >= 0) {
            if (scale.x > 0) {
                scale.x *= -1.0F;
            }
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;
            angle.z = 310;// 40.0F;
        } else {
            if (scale.x < 0) {
                scale.x *= -1.0F;
            }
            //gameObject.GetComponent<SpriteRenderer>().flipX = false;
            angle.z = 310 - 270;//90.0F - 40.0F;
        }
        gameObject.transform.localEulerAngles = angle;
        gameObject.transform.localScale = scale;
    }

    void MoveFirefly() {
        Vector3 startPosition = butterflyPath[pathPoint];
        Vector3 endPosition = (pathPoint + 1 < butterflyPath.Length) ? butterflyPath[pathPoint + 1] : butterflyPath[0];

        float dist = Vector3.Distance(startPosition, endPosition);
        float currSpeed = speed;
        if (dist != 0) {
            currSpeed = speed / dist;
        }

        timer += Time.deltaTime * currSpeed;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
        if (timer >= 1.0F) {
            pathPoint++;
            timer = 0.0F;
        }

        if (pathPoint >= butterflyPath.Length) {
            pathPoint = 0;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Vector3 currPos = gameObject.transform.position;
        Vector3 nextPos = currPos;
        for (int i = 0; i < path.Length; i++) {
            nextPos += path[i];
            Gizmos.DrawLine(currPos, nextPos);
            currPos += path[i];
        }
        Gizmos.DrawLine(currPos, gameObject.transform.position);
    }

}