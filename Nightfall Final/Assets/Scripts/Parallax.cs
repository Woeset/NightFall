using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

    public Transform[] images;
    public float parallaxScale;
    public float parallaxReductionFactor;
    public float smoothing;

    private Vector3 lastPosition;

	void Start() {
        lastPosition = gameObject.transform.position;
	}
	
	void Update() {
        var parallax = (lastPosition.x - transform.position.x) * parallaxScale;

        for (int i = 0; i < images.Length; i++) {
            var backgroundTargetPosition = images[i].position.x + parallax * (i * parallaxReductionFactor + 1);
            images[i].position = Vector3.Lerp(images[i].position, new Vector3(backgroundTargetPosition, images[i].position.y, images[i].position.z), smoothing * Time.deltaTime);
        }

        lastPosition = transform.position;
	}

}