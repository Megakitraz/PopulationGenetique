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

    public float addForce;

    private float _speed;

    public Creature creature;

    private void Start()
    {
        _allPositions = gameObject.GetComponent<AllPositions>();
        _allPositionsIndex = 0;

        _speed = Random.Range(10, 180);

        StartCoroutine(TargetPositions());
        StartCoroutine(Movement());
    }

    public void CreateArticulationRandomly(int numberOfArticulation)
    {
        _prefabsArticulation = creature._prefabsArticulation;
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

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                articulation.creature = creature;
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly(b);

                break;
            
            case 2:
                b = Random.Range(0, numberOfArticulation - 2 + 1);
                c = numberOfArticulation - 2 - b;

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                articulation.creature = creature;
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly( b);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                articulation.creature = creature;
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly( c);
                break;

            case 3:
                b = Random.Range(0, numberOfArticulation - 3 + 1);
                c = Random.Range(0, numberOfArticulation - 3 - b + 1);
                d = numberOfArticulation - 3 - b - c;

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                articulation.creature = creature;
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly( b);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x , transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                articulation.creature = creature;
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly( c);

                articulation = Instantiate(_prefabsArticulation, new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized, Quaternion.identity, transform).GetComponent<Articulation>();
                linkArticulations.Add(articulation);
                articulation.linkArticulations.Add(this);
                articulation.creature = creature;
                PutArmBetweenArticulation(creature, transform, articulation.transform);
                creature.articulations.Add(articulation);
                articulation.CreateArticulationRandomly( d);
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
                if (i == 0)
                {
                    yield return new WaitForSeconds(_allPositions.allTime[i]);
                }
                else
                {
                    yield return new WaitForSeconds(_allPositions.allTime[i] - _allPositions.allTime[i - 1]);
                }
            }
        }
    }

    IEnumerator Movement()
    {
        float angle = transform.rotation.eulerAngles.z;
        int lastIndex = _allPositionsIndex;
        float timeInLoop = 0;
        while (true)
        {
            if(lastIndex != _allPositionsIndex) timeInLoop = 0;
            else timeInLoop += Time.deltaTime;

            angle = Mathf.MoveTowardsAngle(angle, _allPositions.allAngle[_allPositionsIndex], _speed * Time.deltaTime);

            transform.localRotation = Quaternion.Euler(0,0,angle);
            lastIndex = _allPositionsIndex;
            yield return new WaitForEndOfFrame();
            
        }
    }

    IEnumerator Deplacement()
    {
        //Start
        //Vector3 lastPosition = transform.position;
        Quaternion lastRotation = transform.rotation;
        float deplacement;

        yield return new WaitForEndOfFrame();
        while (true)
        {
            //Update

            addForce = Quaternion.Angle(lastRotation, transform.rotation);

            //lastPosition = transform.position;
            lastRotation = transform.rotation;
            yield return new WaitForEndOfFrame();
        }
    }


}
