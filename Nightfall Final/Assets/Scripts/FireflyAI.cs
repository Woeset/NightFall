using UnityEngine;
using System.Collections;

public class FireflyAI : MonoBehaviour {

    public Vector3[] path;
    public float speed;
    public float tolerance = 2.0F;
    public float rate = 0.025F;
    public float proc = 0.1F;

    private Vector3[] fireflyPath;
    private Light glowLight;
    private float brightness;
    private float glowtimer;
    private float timer;
    private int pathPoint;

	void Start() {
        pathPoint = 0;
        glowtimer = 0.0F;
        timer = 0.0F;
        speed = 1.0F;
        glowLight = gameObject.transform.GetComponentInChildren<Light>();
        brightness = glowLight.intensity;

        Vector3 lastPos = gameObject.transform.position;
        fireflyPath = new Vector3[path.Length + 1];
        for (int i = 0; i < path.Length; i++) {
            lastPos += path[i];
            fireflyPath[i] = lastPos;
        }
        fireflyPath[fireflyPath.Length - 1] = gameObject.transform.position;
    }

    void Update() {
        FixFacing();
        MoveFirefly();
        UpdateGlow();
    }

    void FixFacing() {
        Vector3 startPosition = fireflyPath[pathPoint];
        Vector3 endPosition = (pathPoint + 1 < fireflyPath.Length) ? fireflyPath[pathPoint + 1] : fireflyPath[0];
        if (endPosition.x - startPosition.x >= 0) {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void MoveFirefly() {
        Vector3 startPosition = fireflyPath[pathPoint];
        Vector3 endPosition = (pathPoint + 1 < fireflyPath.Length) ? fireflyPath[pathPoint + 1] : fireflyPath[0];

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

        if (pathPoint >= fireflyPath.Length) {
            pathPoint = 0;
        }
    }

    void UpdateGlow() {
        glowtimer += Time.deltaTime;
        if (glowtimer > proc) {
            float rand = Random.Range(-rate, rate);
            if (glowLight.intensity + rand >= brightness - tolerance && glowLight.intensity + rand <= brightness + tolerance) {
                glowLight.intensity += rand;
            }
            glowtimer = 0.0F;
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