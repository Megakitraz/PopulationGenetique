using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.UIElements;
using UnityEngine;

public class Creature : MonoBehaviour
{

    [SerializeField] private int _nbArticulation;

    public GameObject _prefabsArticulation;

    public List<Articulation> articulations;

    [SerializeField] private float _speed;

    [SerializeField] private AnimationCurve _curveOfCloseAngleToSwim;

    private float _allForce;
    [SerializeField] float _swimPower;

    private void Start()
    {
        CreatCreature();
    }

    private void CreatCreature()
    {
        articulations = new List<Articulation> ();

        Articulation mainArticulation = Instantiate(_prefabsArticulation, transform.position, Quaternion.identity, transform).GetComponent<Articulation>();

        articulations.Add(mainArticulation);

        mainArticulation.creature = this;
        mainArticulation.CreateArticulationRandomly(_nbArticulation-1);

    }

    private void LateUpdate()
    {
        AddAllForces();
    }

    private void AddAllForces()
    {
        _allForce = 0;
        foreach(var articulation in articulations)
        {
            _allForce += articulation.addForce;

            foreach(var linkArticulation in articulation.linkArticulations)
            {
                float angle = Quaternion.Angle(articulation.transform.rotation, linkArticulation.transform.rotation);
                float force = 0;
                
                if(angle <= 180)
                {
                    angle = 180 - angle;
                    force = _curveOfCloseAngleToSwim.Evaluate(angle / 180);
                }
                _allForce += force;
            }

        }
    }

}
