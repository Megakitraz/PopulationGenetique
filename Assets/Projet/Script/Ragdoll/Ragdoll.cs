using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ragdoll", menuName = "Ragdoll")]
public class Ragdoll : ScriptableObject
{
    public GameObject prefabsRagdoll;

    public MovementArticulation[] leftShoulder_MovementCycle;
    public MovementArticulation[] leftUpArm_MovementCycle;
    public MovementArticulation[] leftDownArm_MovementCycle;

    public MovementArticulation[] rightShoulder_MovementCycle;
    public MovementArticulation[] rightUpArm_MovementCycle;
    public MovementArticulation[] rightDownArm_MovementCycle;

    public MovementArticulation[] leftArticulation_MovementCycle;
    public MovementArticulation[] leftUpLeg_MovementCycle;
    public MovementArticulation[] leftDownLeg_MovementCycle;

    public MovementArticulation[] rightArticulation_MovementCycle;
    public MovementArticulation[] rightUpLeg_MovementCycle;
    public MovementArticulation[] rightDownLeg_MovementCycle;

    public MovementArticulation[] head_MovementCycle;

    public Ragdoll(GameObject prefabsRagdoll)
    {
        this.prefabsRagdoll = prefabsRagdoll;

        this.leftShoulder_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
        this.leftUpArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
        this.leftDownArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));

        this.rightShoulder_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
        this.rightUpArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
        this.rightDownArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));

        this.leftArticulation_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
        this.leftUpLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
        this.leftDownLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));

        this.rightArticulation_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
        this.rightUpLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
        this.rightDownLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));

        this.head_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 20));
    }

    private MovementArticulation[] RandomMovementArticulations(int length)
    {
        MovementArticulation[] movementArticulations = new MovementArticulation[length];

        for (int i = 0; i < length; i++)
        {
            movementArticulations[i] = new MovementArticulation(UnityEngine.Random.Range(0.1f, 3f), UnityEngine.Random.insideUnitSphere * 5f);
        }


        return movementArticulations;
    }

    public static Ragdoll RagdollWithMutation(Ragdoll ragdollToMutate, int numberOfMutation)
    {
        Ragdoll newRagdoll = ragdollToMutate;
        for (int i = 0; i < numberOfMutation; i++)
        {
            newRagdoll.DoMutation();
        }

        return newRagdoll;
    }

    public void DoMutation()
    {
        int rand = UnityEngine.Random.Range(0, 30);

        if(rand < 15)
        {

        }
        else if(rand < 25)
        {

        }
        else
        {

        }
    }
}

[System.Serializable]
public struct MovementArticulation
{
    public float time;
    public Vector3 strength;

    public MovementArticulation(float time, Vector3 strength)
    {
        this.time = time;
        this.strength = strength;
    }
}
