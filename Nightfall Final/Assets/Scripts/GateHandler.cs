using UnityEngine;
using System.Collections;

public class GateHandler : MonoBehaviour {

    public GameObject behindGate;

    private AnimationController2D frontController;
    private AnimationController2D behindController;
    private bool opened;

    void Start() {
        frontController = gameObject.GetComponent<AnimationController2D>();
        behindController = behindGate.GetComponent<AnimationController2D>();
    }

    void Update() {
	    if (!opened) {
            frontController.setAnimation("Gate_Front_Idle");
            behindController.setAnimation("Gate_Behind_Idle");
        }
    }

    public void Open() {
        opened = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        frontController.setAnimation("Gate_Front_Opening");
        behindController.setAnimation("Gate_Behind_Opening");
    }

}