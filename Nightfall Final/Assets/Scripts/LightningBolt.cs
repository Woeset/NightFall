using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour {

    public float minDelay = 5.0F;
    public float frequency = 0.1F;
    public float procChance = 0.02F;
    public float aftershockProcChance = 0.15F;
    public int aftershockLimit = 2;
    public bool canHaveAftershock = true;

    private bool isAftershock;
    private float aftershockNum;
    private float flashTimer;
    private float timer;

    void Start() {
        isAftershock = false;
        aftershockNum = 0;
        flashTimer = 0.0F;
        timer -= minDelay;
    }

    void Update() {
	    if (timer >= frequency) {
            timer = 0.0F;
            if (Random.Range(1, (int) (1 / procChance)) == 1) {
                SpawnLightning();
                timer = minDelay;
            }
        }
        timer += Time.deltaTime;
        UpdateLightning();
	}

    void SpawnLightning() {
        ChangeLayers();
        aftershockNum = 0;
        isAftershock = false;
        SetAllChildren(true);
    }

    void ChanceAfterShock() {
        if (canHaveAftershock && aftershockNum < aftershockLimit) {
            if (Random.Range(1, (int) (1 / aftershockProcChance)) == 1) {
                ChangeLayers();
                aftershockNum++;
                isAftershock = true;
                SetAllChildren(true);
            }
        }
    }

    void UpdateLightning() {
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).gameObject.activeSelf == true) {
            if (!isAftershock) {
                if (flashTimer == 0.0F) {
                    //Light up
                    for (int i = 0; i < gameObject.GetComponentsInChildren<Light>().Length; i++) {
                        gameObject.GetComponentsInChildren<Light>()[i].intensity = 8.0F;
                    }
                } else if (flashTimer < 0.2F) {
                    //Light down fast (8 > 2)
                    for (int i = 0; i < gameObject.GetComponentsInChildren<Light>().Length; i++) {
                        gameObject.GetComponentsInChildren<Light>()[i].intensity = 6.0F * ((0.2F - flashTimer) * 5.0F) + 2.0F;
                    }
                } else if (flashTimer < 0.3F) {
                    //Light down (2 > 0)
                    for (int i = 0; i < gameObject.GetComponentsInChildren<Light>().Length; i++) {
                        gameObject.GetComponentsInChildren<Light>()[i].intensity = 2.0F * ((0.3F - flashTimer) * 10.0F);
                    }
                } else {
                    SetAllChildren(false);
                    ChanceAfterShock();
                    flashTimer = 0.0F;
                }
            } else {
                if (flashTimer == 0.0F) {
                    //Light up
                    for (int i = 0; i < gameObject.GetComponentsInChildren<Light>().Length; i++) {
                        gameObject.GetComponentsInChildren<Light>()[i].intensity = 1.0F / aftershockNum;
                    }
                } else if (flashTimer < 0.2F) {
                    //Light down fast (1 > 0.25)
                    for (int i = 0; i < gameObject.GetComponentsInChildren<Light>().Length; i++) {
                        gameObject.GetComponentsInChildren<Light>()[i].intensity = 0.75F / aftershockNum * ((0.2F - flashTimer) * 5.0F) + 0.25F / aftershockNum;
                    }
                } else if (flashTimer < 0.3F) {
                    //Light down (0.25 > 0)
                    for (int i = 0; i < gameObject.GetComponentsInChildren<Light>().Length; i++) {
                        gameObject.GetComponentsInChildren<Light>()[i].intensity = 0.25F / aftershockNum * ((0.3F - flashTimer) * 10.0F);
                    }
                } else {
                    SetAllChildren(false);
                    ChanceAfterShock();
                    flashTimer = 0.0F;
                }
            }
            flashTimer += Time.deltaTime;
        } else {
            flashTimer = 0.0F;
        }
    }

    void SetAllChildren(bool activity) {
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            gameObject.transform.GetChild(i).gameObject.SetActive(activity);
        }
    }

    void ChangeLayers() {
        int layer = RandomLayer();
        gameObject.layer = layer;
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            gameObject.transform.GetChild(i).gameObject.layer = layer;
        }
    }

    int RandomLayer() {
        int rand = Random.Range(0, 3);
        if (rand == 0) {
            //Background
            return 11;
        } else if (rand == 1) {
            //Midground
            return 14;
        } else if (rand == 2) {
            //Midforeground
            return 15;
        } else {
            //Foreground
            return 13;
        }
    }

}