using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        this.leftShoulder_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
        this.leftUpArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
        this.leftDownArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));

        this.rightShoulder_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
        this.rightUpArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
        this.rightDownArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));

        this.leftArticulation_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
        this.leftUpLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
        this.leftDownLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));

        this.rightArticulation_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
        this.rightUpLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
        this.rightDownLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));

        this.head_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
    }

    private MovementArticulation[] RandomMovementArticulations(int length)
    {
        MovementArticulation[] movementArticulations = new MovementArticulation[length];

        for (int i = 0; i < length; i++)
        {
            movementArticulations[i] = RandomMovementArticulation();
        }


        return movementArticulations;
    }

    private MovementArticulation RandomMovementArticulation()
    {
        return new MovementArticulation(UnityEngine.Random.Range(0.8f, 5f), UnityEngine.Random.insideUnitSphere * 3f);
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
            DoMutationChangeCycle();
        }
        else if(rand < 25)
        {
            DoMutationAddCycle();
        }
        else
        {
            DoMutationChangeMovementArticulation();
        }
    }

    private void DoMutationChangeMovementArticulation()
    {
        switch (UnityEngine.Random.Range(0, 12))
        {
            case 0:
                leftShoulder_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 1:
                leftUpArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 2:
                leftDownArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 3:
                rightShoulder_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 4:
                rightUpArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 5:
                rightDownArm_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 6:
                leftArticulation_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 7:
                leftUpLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 8:
                leftDownLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 9:
                rightArticulation_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 10:
                rightUpLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 11:
                rightDownLeg_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;
            case 12:
                head_MovementCycle = RandomMovementArticulations(UnityEngine.Random.Range(1, 5));
                break;

        }
    }

    private void DoMutationAddCycle()
    {
        switch (UnityEngine.Random.Range(0, 12))
        {
            case 0:
                leftShoulder_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 1:
                leftUpArm_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 2:
                leftDownArm_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 3:
                rightShoulder_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 4:
                rightUpArm_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 5:
                rightDownArm_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 6:
                leftArticulation_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 7:
                leftUpLeg_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 8:
                leftDownLeg_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 9:
                rightArticulation_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 10:
                rightUpLeg_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 11:
                rightDownLeg_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;
            case 12:
                head_MovementCycle.Append<MovementArticulation>(RandomMovementArticulation());
                break;

        }
    }

    private void DoMutationChangeCycle()
    {
        int index;
        switch (UnityEngine.Random.Range(0, 12))
        {
            case 0:
                index = UnityEngine.Random.Range(0, leftShoulder_MovementCycle.Length);
                leftShoulder_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                leftShoulder_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 1:
                index = UnityEngine.Random.Range(0, leftUpArm_MovementCycle.Length);
                leftUpArm_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                leftUpArm_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 2:
                index = UnityEngine.Random.Range(0, leftDownArm_MovementCycle.Length);
                leftDownArm_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                leftDownArm_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 3:
                index = UnityEngine.Random.Range(0, rightShoulder_MovementCycle.Length);
                rightShoulder_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                rightShoulder_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 4:
                index = UnityEngine.Random.Range(0, rightUpArm_MovementCycle.Length);
                rightUpArm_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                rightUpArm_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 5:
                index = UnityEngine.Random.Range(0, rightDownArm_MovementCycle.Length);
                rightDownArm_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                rightDownArm_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 6:
                index = UnityEngine.Random.Range(0, leftArticulation_MovementCycle.Length);
                leftArticulation_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                leftArticulation_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 7:
                index = UnityEngine.Random.Range(0, leftUpLeg_MovementCycle.Length);
                leftUpLeg_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                leftUpLeg_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 8:
                index = UnityEngine.Random.Range(0, leftDownLeg_MovementCycle.Length);
                leftDownLeg_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                leftDownLeg_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 9:
                index = UnityEngine.Random.Range(0, rightArticulation_MovementCycle.Length);
                rightArticulation_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                rightArticulation_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 10:
                index = UnityEngine.Random.Range(0, rightUpLeg_MovementCycle.Length);
                rightUpLeg_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                rightUpLeg_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 11:
                index = UnityEngine.Random.Range(0, rightDownLeg_MovementCycle.Length);
                rightDownLeg_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                rightDownLeg_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;
            case 12:
                index = UnityEngine.Random.Range(0, head_MovementCycle.Length);
                head_MovementCycle[index].time = UnityEngine.Random.Range(0.1f, 3f);
                head_MovementCycle[index].strength = UnityEngine.Random.insideUnitSphere * 5f;
                break;

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
