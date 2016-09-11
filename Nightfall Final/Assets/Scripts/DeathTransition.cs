using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathTransition : MonoBehaviour {

    public PlayerController player;
    public GameManager gameManager;
    public GameObject blackPanel;
    public Image screenWipe;

    private float rate = 400;
    private float blackTime = 0.25F;
    private int stage = 0;
    private float wipeTime;

    void Start() {

    }

    void Update() {
        if (blackPanel.activeSelf) {
            gameManager.canPause = false;
            if (stage == 0) {
                if (screenWipe.rectTransform.position.y + rate * Time.deltaTime < Screen.height / 2.0F) {
                    screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, screenWipe.rectTransform.position.y + rate * Time.deltaTime, screenWipe.rectTransform.position.z);
                } else {
                    screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, Screen.height / 2.0F, screenWipe.rectTransform.position.z);
                    stage++;
                }
            } else if (stage == 1) {
                wipeTime += Time.deltaTime;
                if (wipeTime > blackTime) {
                    screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, Screen.height / 2.0F, screenWipe.rectTransform.position.z);
                    wipeTime = 0.0F;
                    stage++;
                    gameManager.RestartLevel();
                }
            }
        }
    }

    public void PlayerDied() {
        player.isDead = true;
        player.deathTimer = 999.0F;
        screenWipe.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height * 1.35F);
        screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, -(Screen.height / 2.0F), screenWipe.rectTransform.position.z);
        blackPanel.SetActive(true);
    }

}