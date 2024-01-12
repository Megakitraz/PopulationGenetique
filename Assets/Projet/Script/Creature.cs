using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Creature : MonoBehaviour
{

    [SerializeField] private int _nbArticulation;

    [SerializeField] private GameObject _prefabsArticulation;

    public Articulation[] articulations;

    private void CreatCreature()
    {
        articulations = new Articulation[_nbArticulation];

        Articulation mainArticulation = Instantiate(_prefabsArticulation, new Vector3(0, 0, 0), Quaternion.identity, transform).GetComponent<Articulation>();

        articulations.Append(mainArticulation);

        mainArticulation.CreateArticulationRandomly(this,_nbArticulation);

    }

}
