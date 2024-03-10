using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.UIElements;
using UnityEngine;

public class Creature : MonoBehaviour
{

    [SerializeField] public int _nbArticulation;

    public GameObject _prefabsArticulation;

    public List<Articulation> articulations;

    [SerializeField] private float _speed;

    [SerializeField] private float _coeficientSpeed;

    private Vector2 _inertie;

    [SerializeField] private AnimationCurve _curveOfCloseAngleToSwim;

    private Vector2 _allForce;
    [SerializeField] float _swimPower;

    private void Start()
    {
        _inertie = Vector2.zero;
    }

    private void OnEnable()
    {
        Start();
    }

    public void CreatCreature()
    {
        articulations = new List<Articulation> ();

        Articulation mainArticulation = Instantiate(_prefabsArticulation, transform.position, Quaternion.identity, transform).GetComponent<Articulation>();

        articulations.Add(mainArticulation);

        mainArticulation.creature = this;
        mainArticulation.CreateArticulationRandomly(_nbArticulation-1);

    }

    public void AddMutation(int numberOfMutation)
    {
        for (int i = 0; i < numberOfMutation; i++)
        {

            int rand = Random.Range(0, 100);

            if (rand < 40) AddAngleOfMovement();
            else if (rand < 80) AddTimeOfMovement();
            else AddArticulation();
            
        }
    }

    public void AddArticulation()
    {
        int randArticulation = Random.Range(0, _nbArticulation);
        articulations[randArticulation].CreateArticulationRandomly(1);
    }

    public void AddTimeOfMovement()
    {
        int randArticulation = Random.Range(0, _nbArticulation);
        float randTimeToAdd = Random.Range(-1f, 1f);
        randTimeToAdd = randTimeToAdd * Mathf.Abs(randTimeToAdd);
        AllPositions allposition = articulations[randArticulation].transform.GetComponent<AllPositions>();
        if (allposition == null) return;
        int randIndexOfListMovement = Random.Range(0, allposition.allTime.Length);
        allposition.allTime[randIndexOfListMovement] += randTimeToAdd;

    }

    public void AddAngleOfMovement()
    {
        int randArticulation = Random.Range(0, _nbArticulation);
        float randAngleToAdd = Random.Range(-1f, 1f);
        randAngleToAdd = randAngleToAdd * Mathf.Abs(randAngleToAdd) * 180;
        AllPositions allposition = articulations[randArticulation].transform.GetComponent<AllPositions>();
        if (allposition == null) return;
        int randIndexOfListMovement = Random.Range(0, allposition.allAngle.Length);
        allposition.allAngle[randIndexOfListMovement] += randAngleToAdd;

    }

    private void LateUpdate()
    {
        AddAllForces();
    }

    private void AddAllForces()
    {
        _allForce = Vector2.zero;
        foreach(var articulation in articulations)
        {
            _allForce += articulation.addForce;

            foreach(var linkArticulation in articulation.linkArticulations)
            {
                float angle = Quaternion.Angle(articulation.transform.rotation, linkArticulation.transform.rotation);
                Vector2 deplacement = Vector2.zero;
                
                if(angle <= 180)
                {
                    angle = 180 - angle;
                    angle = _curveOfCloseAngleToSwim.Evaluate(angle / 180);
                }
                deplacement = Vector2.Perpendicular(articulation.transform.position - linkArticulation.transform.position).normalized;
                //deplacement = (articulation.transform.position - linkArticulation.transform.position).normalized;

                if (Vector2.Dot(deplacement, transform.position) < 0)
                    deplacement = -deplacement;

                _allForce += deplacement * angle;
            }

        }

        CreatureMovement(_allForce);
    }

    private void CreatureMovement(Vector2 deplacement)
    {
        _inertie += deplacement * _speed * Time.deltaTime / _coeficientSpeed;
        Vector2 _frottement = -deplacement.normalized * GameManager.Instance._waterFrottementValue * Mathf.Pow(Time.time,2) / _coeficientSpeed;
        if(_frottement.magnitude > _inertie.magnitude) _inertie = Vector2.zero;
        else _inertie -= _frottement;

        transform.Translate(deplacement * _speed * Time.deltaTime / _coeficientSpeed);
    }

}
