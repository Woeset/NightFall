using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {

    public AnimationController2D houseAnim;
    public LoadTransition endOfScene;
    public PlayerCamera2D playerCamera;
    public GameObject player;
    public GameObject houseOverlay;

    private bool updatedOnce = false;
    private float timer = 0.0F;
    private bool clock = false;

    void Start() {
	    
	}
	
	void Update() {
	    if (player.activeSelf == false && updatedOnce) {
            if (houseAnim.isAnimationFinished() && houseAnim.getAnimation().Equals("Ending 1")) {
                houseAnim.setAnimationRegardless("Ending 2");
            } else if (houseAnim.isAnimationFinished() && houseAnim.getAnimation().Equals("Ending 2")) {
                houseAnim.setAnimationRegardless("Ending 3");
            } else if (houseAnim.isAnimationFinished() && houseAnim.getAnimation().Equals("Ending 3")) {
                clock = true;
            }
        }

        if (clock) {
            timer += Time.deltaTime;
            if (timer >= 1F) {
                timer = 0;
                clock = false;
                playerCamera.stopCameraFollow();
                player.SetActive(true);
                player.gameObject.transform.position += new Vector3(50, 0, 0);
                endOfScene.CompleteLevel();
            }
        }
        updatedOnce = true;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            player.SetActive(false);
            houseOverlay.SetActive(false);
            houseAnim.setAnimationRegardless("Ending 1");
            updatedOnce = false;
        }
    }

}