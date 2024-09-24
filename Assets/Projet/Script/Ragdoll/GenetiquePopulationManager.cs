using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenetiquePopulationManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabsRagdoll;

    private RagdollDepartureArrival[] populationRagdolls;

    private void Start()
    {
        StartCoroutine(DoSimulation());
    }

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
            populationRagdolls[i].departurePosition = new Vector3(distanceBetweenPeople * (i - populationRagdolls.Length / 2f), 5, 0);
            populationRagdolls[i].gameobject = Instantiate(populationRagdolls[i].ragdoll.prefabsRagdoll, populationRagdolls[i].departurePosition, Quaternion.Euler(0, 0, 0));

            var movementRagdoll = populationRagdolls[i].gameobject.GetComponent<MovementRagdoll>();
            movementRagdoll.ragdoll = populationRagdolls[i].ragdoll;
            populationRagdolls[i].movementRagdoll = movementRagdoll;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DeletePopulation()
    {

        for (int i = 0; i < populationRagdolls.Length; i++)
        {
            populationRagdolls[i].arrivalPosition = populationRagdolls[i].gameobject.transform.GetChild(0).position;
            Destroy(populationRagdolls[i].gameobject);

            yield return new WaitForEndOfFrame();
        }
    }

    private void StartPopulationMovement()
    {
        for (int i = 0; i < populationRagdolls.Length; i++)
        {

            populationRagdolls[i].movementRagdoll.StartMovement();
        }
    }



    private void SelectAndCloneAllTheBestPeople()
    {
        // 1. Calculer la distance parcourue pour chaque Ragdoll
        List<RagdollDepartureArrival> sortedPopulation = new List<RagdollDepartureArrival>(populationRagdolls);
        sortedPopulation.Sort((a, b) => Vector3.Distance(b.departurePosition, b.arrivalPosition)
                                        .CompareTo(Vector3.Distance(a.departurePosition, a.arrivalPosition)));

        // 2. Garder uniquement la moitié supérieure de la population
        int halfPopulationCount = sortedPopulation.Count / 2;
        List<RagdollDepartureArrival> bestRagdolls = sortedPopulation.GetRange(0, halfPopulationCount);

        // 3. Mettre à jour la population avec les meilleurs Ragdolls + leurs clones
        populationRagdolls = bestRagdolls.ToArray();
        RagdollDepartureArrival[] cloneRagdolls = new RagdollDepartureArrival[populationRagdolls.Length];

        for (int i = 0; i < populationRagdolls.Length; i++)
        {
            populationRagdolls[i] = new RagdollDepartureArrival(populationRagdolls[i].ragdoll);
            cloneRagdolls[i] = new RagdollDepartureArrival(Ragdoll.RagdollWithMutation(populationRagdolls[i].ragdoll, UnityEngine.Random.Range(1,5)));
        }

        populationRagdolls = populationRagdolls.Concat(cloneRagdolls).ToArray();
        
    }

    IEnumerator DoSimulation()
    {
        InitiatePopulation(100);
        int generation = 0;

        while (true)
        {
            generation++;
            Debug.Log($"Génération {generation}");

            yield return StartCoroutine(PlacePopulation(10));

            yield return new WaitForSeconds(1);

            StartPopulationMovement();

            yield return new WaitForSeconds(10);

            yield return StartCoroutine(DeletePopulation());
            SelectAndCloneAllTheBestPeople();

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
    public MovementRagdoll movementRagdoll;

    public RagdollDepartureArrival(Ragdoll ragdoll)
    {
        this.ragdoll = ragdoll;
        this.departurePosition = new Vector3(0, 0, 0);
        this.arrivalPosition = new Vector3(0, 0, 0);
        this.gameobject = null;
        this.movementRagdoll = null;
    }
}
