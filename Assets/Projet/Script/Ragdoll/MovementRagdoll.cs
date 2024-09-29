using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovementRagdoll : MonoBehaviour
{
    public Ragdoll ragdoll;
    public Transform head;
    public Transform torso;
    private bool recordHeadPosition = true;
    private float recordedHeadHeightAverage;

    [SerializeField] private MovementBodyMember m_leftShoulder;
    [SerializeField] private MovementBodyMember m_leftUpArm;
    [SerializeField] private MovementBodyMember m_leftDownArm;

    [SerializeField] private MovementBodyMember m_rightShoulder;
    [SerializeField] private MovementBodyMember m_rightUpArm;
    [SerializeField] private MovementBodyMember m_rightDownArm;

    [SerializeField] private MovementBodyMember m_leftArticulation;
    [SerializeField] private MovementBodyMember m_leftUpLeg;
    [SerializeField] private MovementBodyMember m_leftDownLeg;

    [SerializeField] private MovementBodyMember m_rightArticulation;
    [SerializeField] private MovementBodyMember m_rightUpLeg;
    [SerializeField] private MovementBodyMember m_rightDownLeg;

    [SerializeField] private MovementBodyMember m_head;

    public void StartMovement()
    {
        m_leftShoulder.StartMovement(ragdoll.leftShoulder_MovementCycle);
        m_leftUpArm.StartMovement(ragdoll.leftUpArm_MovementCycle);
        m_leftDownArm.StartMovement(ragdoll.leftDownArm_MovementCycle);

        m_rightShoulder.StartMovement(ragdoll.rightShoulder_MovementCycle);
        m_rightUpArm.StartMovement(ragdoll.rightUpArm_MovementCycle);
        m_rightDownArm.StartMovement(ragdoll.rightDownArm_MovementCycle);

        m_leftArticulation.StartMovement(ragdoll.leftArticulation_MovementCycle);
        m_leftUpLeg.StartMovement(ragdoll.leftUpLeg_MovementCycle);
        m_leftDownLeg.StartMovement(ragdoll.leftDownLeg_MovementCycle);

        m_rightArticulation.StartMovement(ragdoll.rightArticulation_MovementCycle);
        m_rightUpLeg.StartMovement(ragdoll.rightUpLeg_MovementCycle);
        m_rightDownLeg.StartMovement(ragdoll.rightDownLeg_MovementCycle);

        m_head.StartMovement(ragdoll.head_MovementCycle);
    }

    public float timeStanding;

    private void Update()
    {
        if (IsUpright())
        {
            timeStanding += Time.deltaTime;
        }
        else
        {
            timeStanding = 0;  // Reset if the ragdoll falls
        }
    }

    private bool IsUpright()
    {
        return head.transform.position.y > torso.transform.position.y && Mathf.Abs(torso.transform.rotation.x) < 10f;
    }

    IEnumerator CalculateAveragePositionHead()
    {
        int frame = 0;
        float headHeight = 0;
        while (recordHeadPosition)
        {
            frame++;

            headHeight += head.position.y;

            yield return new WaitForEndOfFrame();
        }

        recordedHeadHeightAverage = headHeight / (float)frame;
        recordHeadPosition = true;

    }

    public void StartCalculateAveragePositionHead()
    {
        StartCoroutine(CalculateAveragePositionHead());
    }

    public float StopCalculateAveragePositionHead()
    {
        recordHeadPosition = false;
        return recordedHeadHeightAverage;
    }
}