using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Articulation : MonoBehaviour
{
    public List<Articulation> linkArticulations = new List<Articulation>();

    private GameObject _prefabsArticulation;
    [SerializeField] private GameObject _prefabsArm;

    private AllPositions _allPositions;
    private int _allPositionsIndex;

    private void Start()
    {
        _allPositions = gameObject.GetComponent<AllPositions>();
        _allPositionsIndex = 0;

        StartCoroutine(TargetPositions());
        StartCoroutine(Movement());
    }

    public void CreateArticulationRandomly(Creature creature, int numberOfArticulation)
    {
        _prefabsArticulation = creature._prefabsArticulation;
        int maximum = 3;
        int minimum = 1;
        if (numberOfArticulation < 3) maximum = numberOfArticulation;
        if (numberOfArticulation < minimum) return;

        int otherLinkArticulation = Random.Range(minimum, maximum);
        int b, c, d;
        Articulation articulation;

        //Debug.Log(gameObject.name+": " + otherLinkArticulation);

        switch (otherLinkArticulation)
        {
            case 1:
                b = numberOfArticulation - 1;

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, b);

                break;
            
            case 2:
                b = Random.Range(0, numberOfArticulation - 2 + 1);
                c = numberOfArticulation - 2 - b;

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, b);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
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

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, b);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x , transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(creature, c);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
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
            transform).transform;
        
        arm.localScale = new Vector3(Vector3.Distance(firstArticulation.position, secondArticulation.position), arm.localScale.y, arm.localScale.z);
    }

    IEnumerator TargetPositions()
    {
        while (true)
        {
            for (int i = 0; i < _allPositions.allTime.Length; i++)
            {
                _allPositionsIndex = i;
                yield return new WaitForSeconds(_allPositions.allTime[i]);
            }
        }
    }

    IEnumerator Movement()
    {
        float angle = transform.rotation.eulerAngles.z;
        while (true)
        {
            if(angle > _allPositions.allAngle[_allPositionsIndex])
            {
                angle -= (_allPositions.allAngle[_allPositionsIndex] / 500000) * Time.deltaTime;
            }
            else
            {
                angle += (_allPositions.allAngle[_allPositionsIndex] / 500000) * Time.deltaTime;
            }
            transform.RotateAround(Vector3.forward,angle);
            yield return new WaitForEndOfFrame();
        }
    }


}
