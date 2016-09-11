using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadTransition : MonoBehaviour {

    public PlayerController player;
    public GameManager gameManager;
    public PlaySoundOnExistence soundPlaying;
    public FlashlightBatteryLife battery;
    public GameObject blackPanel;
    public Image screenWipe;
    public Text text;
    public string message;
    public bool longTransition = false;
    public bool regularTransition = true;

    private Color color;
    private float rate;
    private float blackTime;
    private int stage = 3;
    private float wipeTime;

    void Start() {
        if (message != null) {
            text.text = message;
        }
        if (longTransition) {
            blackTime = 4.0F;
            rate = 400;
        } else {
            blackTime = 10.0F;
            rate = 200;
        }
        wipeTime = 0.0F;
        gameManager.canPause = false;
        blackPanel.SetActive(true);
        color = text.GetComponent<Text>().color;
        color.a = 0.0F;
        screenWipe.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height * 1.35F);
        screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, Screen.height / 2.0F, screenWipe.rectTransform.position.z);
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
                if (longTransition) {
                    if (wipeTime < 1.0F) {
                        color.a = wipeTime;
                    } else {
                        color.a = 1.0F;
                    }
                    if (wipeTime > blackTime - 1.0F) {
                        color.a = blackTime - wipeTime;
                    }
                } else {
                    if (wipeTime < 4.0F) {
                        color.a = wipeTime / 4.0F;
                        if (soundPlaying != null) {
                            soundPlaying.FadeOutSound();
                        }
                    } else {
                        color.a = 1.0F;
                    }
                    if (wipeTime > blackTime - 4.0F) {
                        color.a = (blackTime - wipeTime) / 4.0F;
                    }
                }
                if (wipeTime > blackTime) {
                    screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, Screen.height / 2.0F, screenWipe.rectTransform.position.z);
                    color.a = 0.0F;
                    wipeTime = 0.0F;
                    stage++;
                    if (regularTransition) {
                        gameManager.Continue();
                    } else {
                        gameManager.WipeSave();
                        gameManager.MainMenu();
                    }
                }
            } else {
                if (longTransition) {
                    screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, screenWipe.rectTransform.position.y + rate * Time.deltaTime, screenWipe.rectTransform.position.z);
                } else {
                    screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, screenWipe.rectTransform.position.y + 2 * rate * Time.deltaTime, screenWipe.rectTransform.position.z);
                }
                if (screenWipe.rectTransform.position.y > 2.0F * Screen.height) {
                    blackPanel.SetActive(false);
                    gameManager.canPause = true;
                    wipeTime = 0.0F;
                    stage = 0;
                }
            }
        }
        text.GetComponent<Text>().color = color;
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            CompleteLevel();
        }
    }

    public void CompleteLevel() {
        player.isDead = true;
        player.deathTimer = 999.0F;
        gameManager.IncSave();
        if (battery != null) {
            battery.SaveBatteryLife();
        }
        screenWipe.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height * 1.35F);
        screenWipe.rectTransform.position = new Vector3(screenWipe.rectTransform.position.x, -(Screen.height / 2.0F), screenWipe.rectTransform.position.z);
        blackPanel.SetActive(true);
    }

}