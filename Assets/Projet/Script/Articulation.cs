using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Articulation : MonoBehaviour
{
    public List<Articulation> linkArticulations = new List<Articulation>();

    [SerializeField] private GameObject _prefabsArticulation;
    [SerializeField] private GameObject _prefabsArm;

    public void CreateArticulationRandomly(Creature creature, int numberOfArticulation)
    {
        int maximum = 3;
        int minimum = 1;
        if (numberOfArticulation < 3) maximum = numberOfArticulation;
        if (numberOfArticulation < minimum) return;

        int otherLinkArticulation = Random.Range(minimum, maximum);
        int b, c, d;
        Articulation articulation;

        switch (otherLinkArticulation)
        {
            case 1:
                b = numberOfArticulation - 1;

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, creature.transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, b);

                break;
            
            case 2:
                b = Random.Range(0, numberOfArticulation - 2 + 1);
                c = numberOfArticulation - 2 - b;

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, creature.transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, b);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, creature.transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, c);
                break;

            case 3:
                b = Random.Range(0, numberOfArticulation - 3 + 1);
                c = Random.Range(0, numberOfArticulation - 3 - b + 1);
                d = numberOfArticulation - 3 - b - c;

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, creature.transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, b);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x , transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, creature.transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, c);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, creature.transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, d);
                break;

            default:
                return;
        }
    }

    public void PutArmBetweenArticulation(Creature creature,Transform firstArticulation, Transform secondArticulation)
    {
        Transform arm = Instantiate(_prefabsArm,
            (secondArticulation.position + firstArticulation.position) / 2f,
            Quaternion.FromToRotation(Vector3.right, secondArticulation.position - firstArticulation.position),
            creature.transform).transform;
        
        arm.localScale = new Vector3(Vector3.Distance(firstArticulation.position, secondArticulation.position), arm.localScale.y, arm.localScale.z);
    }


}
