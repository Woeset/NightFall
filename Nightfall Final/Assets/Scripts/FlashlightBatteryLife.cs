using UnityEngine;
using System.Collections;

public class FlashlightBatteryLife : MonoBehaviour {

    public GameManager gameManager;
    public ParticleSystem sparks;
    public float updateRate = 0.05F;
    public float varyingBrightness = 0.05F;

    private PlayerController player;
    private float batteryLife;
    private float batteryBrightness;
    private float brightnessTimer;
    private float timer;

    private Vector3 previousPos;
    private float mouseRecentDist;
    private bool shakeStarted;
    private bool firstDeath;
    private bool tempDead;
    private float surgeFactor;
    private int surge;

    void Start() {
        player = gameObject.GetComponentInParent<PlayerController>();
        batteryLife = gameManager.BatteryLife;
        shakeStarted = gameManager.BatteryDied;
        batteryBrightness = batteryLife;
        brightnessTimer = 0.0F;
        timer = 0.0F;
        mouseRecentDist = 0.0F;
        previousPos = Input.mousePosition;
    }

    void Surge() {
        surgeFactor = 0.5F;
    }

    void Update() {
        mouseRecentDist += Vector3.Distance(previousPos, Input.mousePosition);
        previousPos = Input.mousePosition;
        mouseRecentDist *= 0.85F;
        if (mouseRecentDist > 600.0F) {
            //print(mouseRecentDist);
            if (tempDead) {
                ShakeStartLight();
            }
        }

        if (!tempDead) {
            //sec of battery life
            batteryLife -= (Time.deltaTime / 120.0F);
            brightnessTimer += Time.deltaTime;

            if (brightnessTimer >= updateRate) {
                brightnessTimer = 0.0F;

                if (batteryLife <= 0) {
                    //stays at 0
                    batteryBrightness = 0;
                    surge = 0;
                } else if (batteryLife <= 0.25F) {
                    //decay the brightness (0.4 > 0)
                    float alteration = 0.4F * (batteryLife) / 0.25F;
                    batteryBrightness = UnityEngine.Random.Range(((alteration - varyingBrightness < 0.0F) ? 0.0F : alteration - varyingBrightness), alteration + varyingBrightness);
                    batteryBrightness -= surgeFactor;
                    if (surge == 2) {
                        surge++;
                        Surge();
                    }
                } else if (batteryLife <= 0.5F) {
                    //decay the brightness (0.8 > 0.5)
                    float alteration = 0.3F * (batteryLife - 0.25F) / 0.25F;
                    batteryBrightness = UnityEngine.Random.Range(0.5F + alteration - varyingBrightness, 0.5F + alteration + varyingBrightness);
                    batteryBrightness -= surgeFactor;
                    if (surge == 1) {
                        surge++;
                        Surge();
                    }
                } else if (batteryLife <= 0.75F) {
                    //decay the brightness (1 > 0.85)
                    float alteration = 0.15F * (batteryLife - 0.5F) / 0.25F;
                    batteryBrightness = UnityEngine.Random.Range(0.85F + alteration - varyingBrightness, 0.85F + alteration + varyingBrightness);
                    batteryBrightness -= surgeFactor;
                    if (surge == 0) {
                        surge++;
                        Surge();
                    }
                } else {
                    //stays at 1
                    batteryBrightness = UnityEngine.Random.Range(1.0F - 2 * varyingBrightness, 1.0F);
                }
            }
            surgeFactor *= 0.98F;

            if (batteryLife <= 0.0F) {
                batteryLife = 0.0F;
                timer += Time.deltaTime;
            }
            if (timer > 3.0F) {
                gameManager.BatteryLife = 1.0F;
                gameManager.BatteryDied = false;
                player.PlayerDeath();
            }
            if (batteryLife <= 0.25F && !shakeStarted) {
                player.FlashlightDied();
                mouseRecentDist = 0;
                sparks.Emit(5);
                tempDead = true;
            }
        }
    }

    public void ShakeStartLight() {
        if (batteryLife <= 0.25F && tempDead) {
            batteryLife = 0.75F;
            shakeStarted = true;
            tempDead = false;
        }
    }

    public float GetBatteryLife() {
        if (batteryLife <= 0.25F) {
            if (tempDead) {
                return 0.05F;
            } else {
                return batteryBrightness;
            }
        }
        return batteryBrightness;
    }

    public void BatteryPickup() {
        batteryLife = 1.0F;
        shakeStarted = false;
    }

    public void SaveBatteryLife() {
        gameManager.BatteryLife = batteryLife;
        gameManager.BatteryDied = shakeStarted;
    }

}