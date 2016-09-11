using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour {

    public PlaySoundOnce attackSound;
    public float speed = 1.0F;
    public bool keepMoving = false;
    public bool instantBounceBack = false;
    public bool stopAtWalls = false;
    public bool improvedZ1 = true;
    public bool improvedZ2 = true;
    public bool canFlip = false;

    private PlayerController controller;
    private AnimationController2D animator;
    private Rigidbody2D rb2D;
    private GameObject player;
    private Vector2 monsterPos;
    private Vector2 playerPos;
    private Vector2 previousPos;
    private float lastXPos;
    private float xMovementDirection;
    private float xDisp;

    private bool canMove = true;
    private bool attacking;

    void Start() {
        animator = gameObject.GetComponent<AnimationController2D>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        previousPos = gameObject.transform.position;
        lastXPos = gameObject.transform.position.x;
        xMovementDirection = 1.0F;
        xDisp = 0.0F;
    }
	
	void Update() {
        if (player != null && !controller.isDead && canMove) {
            monsterPos = gameObject.transform.position;
            playerPos = player.transform.position;

            if (!keepMoving || xDisp == 0.0F) {
                xDisp = playerPos.x - monsterPos.x;
            }
            float move = Time.deltaTime * speed;
            bool moving = false;

            if (instantBounceBack) {
                RaycastHit2D[] raycasts = Physics2D.RaycastAll(transform.position, player.transform.position, 2.5F);
                for (int i = 0; i < raycasts.Length; i++) {
                    if (raycasts[i].rigidbody != null && (raycasts[i].rigidbody.gameObject.layer == 0 || raycasts[i].rigidbody.gameObject.layer == 8)) {
                        xMovementDirection = -1;
                    }
                }
            }

            if (stopAtWalls) {
                float xMovement = gameObject.transform.position.x - lastXPos;
                lastXPos = gameObject.transform.position.x;
                if (xMovement < 0.0F) {
                    canMove = false;
                }
            }

            if (improvedZ1) {
                RaycastHit2D[] raycasts = Physics2D.RaycastAll(transform.position, player.transform.position, 2.5F);
                for (int i = 0; i < raycasts.Length; i++) {
                    if (raycasts[i].rigidbody != null && raycasts[i].rigidbody.gameObject.layer == 8) {
                        //rb2D.AddForce(new Vector2(0, 0.15F));
                        gameObject.transform.position += new Vector3(0, 0.15F, 0);
                        moving = true;
                    }
                }
            }
            if (improvedZ2) {
                float zRotation = gameObject.transform.eulerAngles.z;
                if (zRotation > 35 && zRotation <= 180) {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, zRotation - 1.5F);
                } else if (zRotation > 180 && gameObject.transform.rotation.z < 325) {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, zRotation + 1.5F);
                }
            }

            if (xDisp >= 0.1F) {
                gameObject.transform.position += new Vector3(move, 0, 0);
                moving = true;
                if (canFlip) {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            } else if (xDisp <= -0.1F) {
                gameObject.transform.position -= new Vector3(move, 0, 0);
                moving = true;
                if (canFlip) {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }

            if (attacking) {
                animator.setAnimation("Attack");
                attackSound.PlayOnce();
            } else if (moving) {
                animator.setAnimation("Walk");
            } else {
                animator.setAnimation("Idle");
            }
        } else {
            if (attacking) {
                animator.setAnimation("Attack");
                attackSound.PlayOnce();
            } else {
                animator.setAnimation("Idle");
            }
        }
    }

    public void OnTrigger(GameObject player) {
        this.player = player;
        controller = player.GetComponent<PlayerController>();
    }

    public void Attack() {
        attacking = true;
    }

}