using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Creature : MonoBehaviour
{

    [SerializeField] private int _nbArticulation;

    public GameObject _prefabsArticulation;

    public List<Articulation> articulations;

    private void Start()
    {
        CreatCreature();
    }

    private void CreatCreature()
    {
        articulations = new List<Articulation> ();

        Articulation mainArticulation = Instantiate(_prefabsArticulation, transform.position, Quaternion.identity, transform).GetComponent<Articulation>();

        articulations.Add(mainArticulation);

        mainArticulation.CreateArticulationRandomly(this,_nbArticulation-1);

    }

}
