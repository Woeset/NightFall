using UnityEngine;
using System.Collections;

public class AnimateButterfly : MonoBehaviour {

    public bool isSideView;

    private AnimationController2D animator;
    
	void Start() {
        animator = gameObject.GetComponent<AnimationController2D>();
    }
	
	void Update() {
	    if (isSideView) {
            animator.setAnimation("Butterfly Side");
        } else {
            animator.setAnimation("Butterfly Front");
        }
    }

}