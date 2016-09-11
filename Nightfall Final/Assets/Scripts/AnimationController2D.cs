using UnityEngine;
using System.Collections;

public class AnimationController2D : MonoBehaviour {

    private string _currentDirection = "Right";
    private string _currentAnimation;
    private Animator _animator;
    private bool canPlay = true;

    void Awake() {
        _animator = this.GetComponent<Animator>();
    }

    public string getFacing() { return _currentDirection; }
    public string getAnimation() {
        if (_currentAnimation == null) {
            return "";
        }
        return _currentAnimation;
    }

    public bool isAnimationFinished() {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0F;
    }

    public void setFacing(string newDirection) {
        //Check that were' not already facing the new direction
        if (newDirection != _currentDirection) {

            //Flip the character sprite
            this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            //Update the member variable that tracks the facing direction
            _currentDirection = newDirection;
        }
    }

    public void setAnimation(string animationName) {
        int hash = Animator.StringToHash(animationName);

        if (canPlay && _animator != null && _animator.HasState(0, hash)) {
            //Check that we're not already playing the animation 
            if (animationName != _currentAnimation) {

                //Set the animation to play in the animator
                _animator.Play(hash);

                //Update the member variable that tracks the character action state
                _currentAnimation = animationName;
            }
        }
    }

    public void setAnimationRegardless(string animationName) {
        int hash = Animator.StringToHash(animationName);
        _animator.ForceStateNormalizedTime(0);

        if (canPlay && _animator != null && _animator.HasState(0, hash)) {
            //Set the animation to play in the animator
            _animator.Play(hash);

            //Update the member variable that tracks the character action state
            _currentAnimation = animationName;
        }
    }

    public void setPermanentAnimation(string animationName) {
        canPlay = false;
        int hash = Animator.StringToHash(animationName);

        if (_animator != null && _animator.HasState(0, hash)) {
            //Check that we're not already playing the animation 
            if (animationName != _currentAnimation) {

                //Set the animation to play in the animator
                _animator.Play(hash);

                //Update the member variable that tracks the character action state
                _currentAnimation = animationName;
            }
        }
    }

}