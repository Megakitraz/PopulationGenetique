using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenetiquePopulationManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabsRagdoll;

    private RagdollDepartureArrival[] populationRagdolls;

    private void InitiatePopulation(int sizePopulation)
    {
        populationRagdolls = new RagdollDepartureArrival[sizePopulation];
        for (int i = 0; i < sizePopulation; i++)
        {
            populationRagdolls[i] = new RagdollDepartureArrival(new Ragdoll(prefabsRagdoll));
        }
    }

    IEnumerator PlacePopulation(float distanceBetweenPeople)
    {

        for (int i = 0; i < populationRagdolls.Length; i++)
        {
            populationRagdolls[i].gameobject = Instantiate(populationRagdolls[i].ragdoll.prefabsRagdoll, new Vector3(distanceBetweenPeople * (i - populationRagdolls.Length/2f), 0,0), Quaternion.Euler(0, 0, 0));

            yield return new WaitForEndOfFrame();
        }
    }


}
public struct RagdollDepartureArrival
{
    public Vector3 departurePosition;
    public Vector3 arrivalPosition;
    public Ragdoll ragdoll;
    public GameObject gameobject;

    public RagdollDepartureArrival(Ragdoll ragdoll)
    {
        this.ragdoll = ragdoll;
        this.departurePosition = new Vector3(0, 0, 0);
        this.arrivalPosition = new Vector3(0, 0, 0);
        this.gameobject = null;
    }
}
