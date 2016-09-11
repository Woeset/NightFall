using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerCamera2D : MonoBehaviour {

    public PlayerController player;

    public float damping = 0.1F;
    public float lookAheadFactor = 0.2F;
    public float lookBelowFactor = 0.5F;
    public float lookAheadReturnSpeed = 0.2F;
    public float lookDownReturnSpeed = 0.5F;
    public float lookAheadMoveThreshold = 0.1F;

    public bool platformSnap = true;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    private Transform currentTarget;
    private bool stopFollow;

    private void Start() {

    }

    private void Update() {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (currentTarget.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
        bool updateLookBelowTarget = player.isCrouching;
        m_LookAheadPos = Vector3.zero;

        if (updateLookAheadTarget) {
            m_LookAheadPos += lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        } else {
            m_LookAheadPos += Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }
        if (!stopFollow) {
            if (updateLookBelowTarget) {
                m_LookAheadPos += lookBelowFactor * Vector3.down;
            } else {
                m_LookAheadPos += Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookDownReturnSpeed);
            }
        }

        /*if (updateLookAheadTarget && updateLookBelowTarget) {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta) + lookBelowFactor * Vector3.down;
        } else if (updateLookAheadTarget) {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        } else if (updateLookBelowTarget) {
            m_LookAheadPos = lookBelowFactor * Vector3.down;
        } else {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }*/

        Vector3 aheadTargetPos = currentTarget.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;

        m_LastTargetPosition = currentTarget.position;
    }

    public void startCameraFollow(GameObject newTarget) {
        stopFollow = false;
        currentTarget = newTarget.transform;
        m_LastTargetPosition = currentTarget.position;
        m_OffsetZ = transform.position.z;
        transform.parent = null;
    }


    public void stopCameraFollow() {
        stopFollow = true;
        currentTarget = this.transform;
        m_LastTargetPosition = currentTarget.position;
        m_OffsetZ = 0;
    }

    public void setDamping(float value) {
        damping = value;
    }

    public void setTarget(GameObject newTarget) {
        currentTarget = newTarget.transform;
    }

}