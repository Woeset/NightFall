using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prime31;

public class PlayerController : MonoBehaviour {

    public GameObject gameCamera;
    public GameManager gameManager;
    public SoundManager soundManager;
    public GameObject flashlight;
    public GameObject hands;
    public GameObject pausePanel;
    public GameObject cameraPosition;
    public DeathTransition deathTransition;
    public GameObject shakenCollectable;
    public Sunset sunset;
    public float walkSpeed = 1.25F;
    public float jumpHeight = 2.0F;
    public float mudWalkSpeed = 0.35F;
    public float mudJumpHeight = 1.0F;
    public float crouchWalkSpeed = 0.5F;
    public float crouchJumpSpeed = 1.25F;
    public float gravity = 50.0F;
    public float friction = 0.82F;
    [HideInInspector]
    public bool isCrouching = false;
    [HideInInspector]
    public float deathTimer;
    [HideInInspector]
    public bool isDead = false;

    private BoxCollider2D boxCollider;
    private CharacterController2D character;
    private AnimationController2D animator;
    private FlashlightBatteryLife batteryLife;
    private Vector2 standingOffset;
    private Vector2 standingSize;
    private Vector2 crouchingOffset;
    private Vector2 crouchingSize;
    private int hitWebCount = 1;
    private int hitHorseCount = 1;
    private float pauseTime;
    private float idleTime;

    private bool hasKey = false;
    private bool isInCutscene = false;
    private bool cutsceneEnded = false;
    private bool isInMud = false;
    private bool facingRight = true;
    private bool isSwinging = false;

    void Start() {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        character = gameObject.GetComponent<CharacterController2D>();
        animator = gameObject.GetComponent<AnimationController2D>();
        batteryLife = gameObject.GetComponentInChildren<FlashlightBatteryLife>();

        gameManager.InitPlayerController(this);
        gameCamera.GetComponent<PlayerCamera2D>().startCameraFollow(cameraPosition);
        if (pausePanel != null) {
            pausePanel.SetActive(false);
        }

        standingOffset = new Vector2(0, -0.12F);
        standingSize = new Vector2(1, 2.25F);
        crouchingOffset = new Vector2(0, -0.37F);
        crouchingSize = new Vector2(1, 1.75F);

        idleTime = 0.0F;
        pauseTime = 0.0F;
        facingRight = true;
        isDead = false;
    }

    void Update() {
        Vector3 velocity = character.velocity;
        bool noAnim = true;

        if (!isDead && !isInCutscene) {
            if (Input.GetKeyDown(KeyCode.Escape) && pausePanel != null && gameManager.canPause) {
                //PlayerDeath();
                SetPauseAlpha(0);
                pauseTime = 0.0F;
                pausePanel.SetActive(true);
                gameManager.SetPaused();
            }

            bool canJump = true;

            if (character.isGrounded && character.ground != null) {
                if (character.ground.tag.Equals("MovingPlatform")) {
                    if (!isSwinging) {
                        gameObject.transform.localRotation = character.ground.transform.localRotation;
                    }
                    isSwinging = true;
                    gameObject.transform.parent = character.ground.transform;
                } else {
                    isSwinging = false;
                    Vector3 angle = gameObject.transform.localEulerAngles;
                    angle.z = 0;
                    gameObject.transform.localEulerAngles = angle;
                }
                if (character.ground.tag.Equals("Mud")) {
                    isInMud = true;
                }

                if (character.ground.tag.Equals("Prevent Jump")) {
                    canJump = false;
                }

                if (character.ground.tag.Equals("Web")) {
                    velocity.y += hitWebCount <= 5 ? 20.0F / hitWebCount : 0.0F;
                    if (hitWebCount <= 2) {
                        if (!animator.getAnimation().Equals("Crouch")) {
                            animator.setAnimationRegardless("Jump");
                        }
                    }
                    hitWebCount++;
                } else {
                    hitWebCount = 1;
                }

                if (character.ground.tag.Equals("Horse")) {
                    velocity.y = hitHorseCount <= 2 ? 10.5F / hitHorseCount : 0.0F;
                    if (hitHorseCount <= 2) {
                        if (!animator.getAnimation().Equals("Crouch")) {
                            animator.setAnimationRegardless("Jump");
                        }
                    }
                    hitHorseCount++;
                } else {
                    hitHorseCount = 1;
                }
            } else {
                if (gameObject.transform.parent != null) {
                    gameObject.transform.parent = null;
                }
            }

            if (Input.GetAxis("Horizontal") < 0.0F) {
                if (isCrouching) {
                    velocity.x -= isInMud ? mudWalkSpeed : crouchWalkSpeed;
                } else {
                    velocity.x -= isInMud ? mudWalkSpeed : walkSpeed;
                }
                animator.setFacing("Left");
                if (character.isGrounded && !isCrouching) {
                    animator.setAnimation("Walk");
                }
                facingRight = false;
                noAnim = false;
            }
            if (Input.GetAxis("Horizontal") > 0.0F) {
                if (isCrouching) {
                    velocity.x += isInMud ? mudWalkSpeed : crouchWalkSpeed;
                } else {
                    velocity.x += isInMud ? mudWalkSpeed : walkSpeed;
                }
                animator.setFacing("Right");
                if (character.isGrounded && !isCrouching) {
                    animator.setAnimation("Walk");
                }
                facingRight = true;
                noAnim = false;
            }

            if (Input.GetAxis("Vertical") < 0.0F) {
                boxCollider.offset = crouchingOffset;
                boxCollider.size = crouchingSize;
                character.recalculateDistanceBetweenRays();
                if (Input.GetAxis("Horizontal") != 0.0F) {
                    animator.setAnimation("Crouch");
                } else {
                    animator.setAnimation("Crouch Idle");
                }
                isCrouching = true;
                noAnim = false;
            } else {
                boxCollider.offset = standingOffset;
                boxCollider.size = standingSize;
                character.recalculateDistanceBetweenRays();
                isCrouching = false;
            }

            if (Input.GetAxis("Jump") > 0.0F && character.isGrounded && canJump && !isCrouching) {
                velocity.y = isInMud ? Mathf.Sqrt(2F * mudJumpHeight * gravity) : (isCrouching ? Mathf.Sqrt(2F * crouchJumpSpeed * gravity) : Mathf.Sqrt(2F * jumpHeight * gravity));
                if (!animator.getAnimation().Equals("Crouch")) {
                    animator.setAnimationRegardless("Jump");
                }
                hitWebCount = 1;
                hitHorseCount = 1;
                noAnim = false;
                if (character.isGrounded && character.ground != null) {
                    if (character.ground.tag == "Web") {
                        velocity.y += 5.0F;
                    }
                }
            }

            if (noAnim && character.isGrounded) {
                if (idleTime > 3.0F) {
                    animator.setAnimation("Idle Blink");
                    if (animator.isAnimationFinished()) {
                        idleTime = 0.0F;
                    }
                } else {
                    if (animator.isAnimationFinished()) {
                        animator.setAnimation("Idle");
                    } else if (animator.getAnimation() == null || (!animator.getAnimation().Equals("Idle") && !animator.getAnimation().Equals("Idle Blink"))) {
                        animator.setAnimation("Idle");
                    }
                }
                idleTime += Time.deltaTime;
            } else {
                idleTime = 0.0F;
            }

            RotateFlashlight();
        } else if (isInCutscene && !isDead) {
            if (!cutsceneEnded) {
                velocity.x += walkSpeed;
                animator.setFacing("Right");
                if (character.isGrounded) {
                    animator.setAnimation("Walk");
                }
                facingRight = true;

                if (Input.GetAxis("Jump") > 0.0F && character.isGrounded) {
                    velocity.y = isInMud ? Mathf.Sqrt(2F * mudJumpHeight * gravity) : (isCrouching ? Mathf.Sqrt(2F * crouchJumpSpeed * gravity) : Mathf.Sqrt(2F * jumpHeight * gravity));
                    if (!animator.getAnimation().Equals("Crouch")) {
                        animator.setAnimationRegardless("Jump");
                    }
                    noAnim = false;
                }

                hands.transform.rotation = Quaternion.Euler(0, 0, 0);
                hands.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.3F + (animator.getAnimation().Equals("Jump") ? 0.26F : 0.0F), this.transform.position.z);
            } else {
                animator.setAnimation("Idle");
            }
        } else if (isDead) {
            if (pausePanel != null && pausePanel.activeSelf == true) {
                pauseTime += 0.01F;
                if (pauseTime < 0.0F) {
                    SetPauseAlpha(-pauseTime * 2);
                } else if (pauseTime <= 0.941F/2) {
                    SetPauseAlpha(pauseTime * 2);
                } else {
                    SetPauseAlpha(0.941F);
                }
                if (pauseTime <= 0.0F && pauseTime >= -0.11F) {
                    pausePanel.SetActive(false);
                    gameManager.SetUnpaused();
                }
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    pauseTime = -0.5F;
                }
            } else {
                deathTimer -= Time.deltaTime;
                if (deathTimer <= 0.0F) {
                    deathTransition.PlayerDied();
                }
            }
        }

        LockZPosition();

        //External Forces
        velocity.x *= friction;
        velocity.y += -gravity * Time.deltaTime;

        //Variable Reset
        isInMud = false;

        //Update Location
        character.move(velocity * Time.deltaTime);
    }

    void SetPauseAlpha(float alpha) {
        Image[] images = pausePanel.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) {
            Color color = images[i].color;
            color.a = alpha;
            images[i].color = color;
        }
    }

    void LockZPosition() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.0F);
    }

    void RotateFlashlight() {
        float mouseRotation = GetMouseRotation();
        mouseRotation *= Mathf.Rad2Deg;
        Vector3 flashlightAngle = flashlight.transform.eulerAngles;
        Vector3 handsAngle = flashlight.transform.eulerAngles;
        flashlightAngle.z = mouseRotation;
        handsAngle.z = GetHandsRotation(mouseRotation);
        flashlight.transform.rotation = Quaternion.Euler(flashlightAngle);
        flashlight.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.3F, this.transform.position.z);
        hands.transform.rotation = Quaternion.Euler(handsAngle);
        hands.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.3F - (isCrouching ? 0.26F : 0.0F) + (animator.getAnimation().Equals("Jump") ? 0.26F : 0.0F), this.transform.position.z);
    }

    float GetHandsRotation(float mouseRotation) {
        if (facingRight) {
            return mouseRotation;
        } else {
            if (mouseRotation > 270.0F || mouseRotation < 90.0F) {
                if (mouseRotation > 0.0F && mouseRotation < 180.0F) {
                    return 180 - mouseRotation;
                } else if (mouseRotation < 360.0F && mouseRotation > 180.0F) {
                    return 180 - mouseRotation;
                } else {
                    return 0.0F;
                }
            } else if (mouseRotation < 270.0F && mouseRotation > 90.0F) {
                if (mouseRotation > 0.0F && mouseRotation < 180.0F) {
                    return 180.0F - mouseRotation;
                } else if (mouseRotation < 360.0F && mouseRotation > 180.0F) {
                    return 180.0F - mouseRotation;
                } else {
                    return 180.0F;
                }
            } else {
                return mouseRotation;
            }
        }
    }

    float GetMouseRotation() {
        float mouseRotation = 0.0F;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xDisp = mousePosition.x - flashlight.transform.position.x;
        float yDisp = mousePosition.y - flashlight.transform.position.y;
        if (xDisp > 0.0F) {
            if (yDisp > 0.0F) {
                mouseRotation = Mathf.Atan(yDisp / xDisp);
            } else if (yDisp < 0.0F) {
                mouseRotation = 2 * Mathf.PI + Mathf.Atan(yDisp / xDisp);
            } else {
                mouseRotation = 0.0F;
            }
        } else if (xDisp < 0.0F) {
            if (yDisp > 0.0F) {
                mouseRotation = Mathf.PI + Mathf.Atan(yDisp / xDisp);
            } else if (yDisp < 0.0F) {
                mouseRotation = Mathf.PI + Mathf.Atan(yDisp / xDisp);
            } else {
                mouseRotation = Mathf.PI;
            }
        } else {
            if (yDisp > 0.0F) {
                mouseRotation = Mathf.Atan(yDisp / xDisp);
            } else if (yDisp < 0.0F) {
                mouseRotation = Mathf.Atan(yDisp / xDisp);
            } else {
                //Exact location
                mouseRotation = 0.0F;
            }
        }
        return mouseRotation;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("KillZ")) {
            PlayerFallDeath();
        } else if (collider.tag.Equals("Collectable")) {
            collider.transform.gameObject.tag = "Collected";
            string type = collider.transform.gameObject.GetComponent<Collectable>().GetCollectableType();
            if (type.Equals("Battery")) {
                batteryLife.BatteryPickup();
            } else if (type.Equals("Key")) {
                hasKey = true;
            }
        } else if (collider.tag.Equals("Monster")) {
            collider.transform.GetChild(0).gameObject.SetActive(true);
            if (collider.transform.GetComponentInChildren<MonsterAI>() != null) {
                collider.transform.GetComponentInChildren<MonsterAI>().OnTrigger(this.gameObject);
            }
        } else if (collider.tag.Equals("Hurtful")) {
            MonsterAI monster = collider.transform.parent.GetComponentInChildren<MonsterAI>();
            if (monster != null) {
                monster.Attack();
            }
            PlayerDeath();
        } else if (collider.tag.Equals("Gate")) {
            if (hasKey) {
                GateHandler gate = collider.gameObject.GetComponent<GateHandler>();
                gate.Open();
                soundManager.PlaySound("Gate");
                hasKey = false;
            }
        } else if (collider.tag.Equals("Cutscene")) {
            isInCutscene = true;
            if (sunset != null) {
                sunset.TriggerSunset();
            }
        } else if (collider.tag.Equals("Stop Player")) {
            cutsceneEnded = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        
    }

    void OnTriggerExit2D(Collider2D collider) {
        
    }

    public void Unpaused() {
        pausePanel.SetActive(false);
    }

    public void FlashlightDied() {
        if (!gameManager.FirstDeath && shakenCollectable != null) {
            gameManager.FirstDeath = true;
            gameManager.BatteryDied = false;
            shakenCollectable.SetActive(true);
            shakenCollectable.tag = "Collected";
        }
    }

    public void PlayerDeath() {
        if (!isDead) {
            if (gameManager.Permadeath) {
                gameManager.WipeSave();
            }
            hands.SetActive(false);
            gameCamera.GetComponent<PlayerCamera2D>().stopCameraFollow();
            animator.setPermanentAnimation("Death");
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + ((facingRight) ? 1 : -1) * -1.0F, gameObject.transform.position.y, gameObject.transform.position.z);
            deathTimer = 2.5F;
            isDead = true;
        }
    }

    void PlayerFallDeath() {
        PlayerDeath();
    }

}