using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GenetiquePopulationManager : MonoBehaviour
{
    [SerializeField] private int sizeOfThePopulation;
    [SerializeField] private int numberOfGroups = 4; // Nouveau : nombre de groupes

    [SerializeField] private GameObject prefabsRagdoll;
    [SerializeField] private GameObject prefabsTarget;

    [SerializeField] private Transform parentRagdoll;
    [SerializeField] private Transform parentTarget;

    [SerializeField] private TextMeshProUGUI generationCount;

    private RagdollDepartureArrival[] populationRagdolls;
    private List<RagdollDepartureArrival[]> groups;

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

        // Nouveau : Diviser la population en groupes
        groups = new List<RagdollDepartureArrival[]>();
        int groupSize = Mathf.CeilToInt((float)sizePopulation / numberOfGroups);

        for (int i = 0; i < numberOfGroups; i++)
        {
            groups.Add(populationRagdolls.Skip(i * groupSize).Take(groupSize).ToArray());
        }
    }

    private void PlaceTargets(float distanceBetweenPeople)
    {
        foreach (var group in groups)
        {
            for (int i = 0; i < group.Length; i++)
            {
                Instantiate(prefabsTarget, new Vector3(distanceBetweenPeople * (i - group.Length / 2f), 0, 0), Quaternion.Euler(0, 0, 0), parentTarget);
            }
        }
    }

    IEnumerator PlaceGroup(RagdollDepartureArrival[] group, float distanceBetweenPeople)
    {
        for (int i = 0; i < group.Length; i++)
        {
            group[i].departurePosition = new Vector3(distanceBetweenPeople * (i - group.Length / 2f), 0, 0);
            group[i].gameobject = Instantiate(group[i].ragdoll.prefabsRagdoll, group[i].departurePosition, Quaternion.Euler(0, 0, 0), parentRagdoll);

            var movementRagdoll = group[i].gameobject.GetComponent<MovementRagdoll>();
            movementRagdoll.ragdoll = group[i].ragdoll;
            group[i].movementRagdoll = movementRagdoll;

            if (i % 3 == 0) yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DeleteGroup(RagdollDepartureArrival[] group)
    {
        for (int i = 0; i < group.Length; i++)
        {
            group[i].arrivalPosition = group[i].gameobject.transform.GetChild(0).position;
            group[i].headHeightAveragePosition = group[i].movementRagdoll.StopCalculateAveragePositionHead();
            group[i].arrivalHeadPosition = group[i].movementRagdoll.head.transform.position;
            group[i].arrivalTorsoPosition = group[i].movementRagdoll.torso.transform.position;
            group[i].timeStanding = group[i].movementRagdoll.timeStanding;
            Destroy(group[i].gameobject);

            yield return new WaitForEndOfFrame();
        }
    }

    private void StartGroupMovement(RagdollDepartureArrival[] group)
    {
        for (int i = 0; i < group.Length; i++)
        {
            group[i].movementRagdoll.StartMovement();
            group[i].movementRagdoll.StartCalculateAveragePositionHead();
        }
    }

    private void SelectAndCloneAllTheBestPeople()
    {
        List<RagdollDepartureArrival> sortedPopulation = new List<RagdollDepartureArrival>(populationRagdolls);

        sortedPopulation.Sort((a, b) => b.ragdoll.CalculateStandingFitness(b.arrivalHeadPosition, b.arrivalTorsoPosition, b.timeStanding)
                                    .CompareTo(a.ragdoll.CalculateStandingFitness(a.arrivalHeadPosition, a.arrivalTorsoPosition, a.timeStanding)));

        int halfPopulationCount = sortedPopulation.Count / 2;
        List<RagdollDepartureArrival> bestRagdolls = sortedPopulation.GetRange(0, halfPopulationCount);

        populationRagdolls = bestRagdolls.ToArray();
        RagdollDepartureArrival[] cloneRagdolls = new RagdollDepartureArrival[populationRagdolls.Length];

        for (int i = 0; i < populationRagdolls.Length; i++)
        {
            populationRagdolls[i] = new RagdollDepartureArrival(populationRagdolls[i].ragdoll);
            cloneRagdolls[i] = new RagdollDepartureArrival(Ragdoll.RagdollWithMutation(populationRagdolls[i].ragdoll, UnityEngine.Random.Range(1, 5)));
        }

        populationRagdolls = populationRagdolls.Concat(cloneRagdolls).ToArray();

        // Nouveau : Rédiviser la population en groupes
        groups = new List<RagdollDepartureArrival[]>();
        int groupSize = Mathf.CeilToInt((float)populationRagdolls.Length / numberOfGroups);

        for (int i = 0; i < numberOfGroups; i++)
        {
            groups.Add(populationRagdolls.Skip(i * groupSize).Take(groupSize).ToArray());
        }
    }

    IEnumerator DoSimulation()
    {
        int populaionSize = sizeOfThePopulation;
        InitiatePopulation(populaionSize);
        PlaceTargets(10);
        int generation = 0;

        while (true)
        {
            generation++;

            SaveSystem.Instance.Save(generation, populationRagdolls.ToList());

            generationCount.text = $"Génération: {generation}";

            foreach (var group in groups)
            {
                yield return StartCoroutine(PlaceGroup(group, 10));

                yield return new WaitForSeconds(group.Length / 50);

                StartGroupMovement(group);

                yield return new WaitForSeconds(10);

                yield return StartCoroutine(DeleteGroup(group));
            }

            SelectAndCloneAllTheBestPeople();

            yield return new WaitForEndOfFrame();

            
        }
    }
}


public struct RagdollDepartureArrival
{
    public Vector3 departurePosition;
    public Vector3 arrivalPosition;
    public Vector3 arrivalHeadPosition;
    public Vector3 arrivalTorsoPosition;
    public float headHeightAveragePosition;
    public float timeStanding;
    public Ragdoll ragdoll;
    public GameObject gameobject;
    public MovementRagdoll movementRagdoll;

    public RagdollDepartureArrival(Ragdoll ragdoll)
    {
        this.ragdoll = ragdoll;
        this.departurePosition = new Vector3(0, 0, 0);
        this.arrivalPosition = new Vector3(0, 0, 0);
        this.arrivalHeadPosition = new Vector3(0, 0, 0);
        this.arrivalTorsoPosition = new Vector3(0, 0, 0);
        this.headHeightAveragePosition = 0;
        this.timeStanding = 0;
        this.gameobject = null;
        this.movementRagdoll = null;
    }
}
