using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float _waterFrottementValue;
    [SerializeField] private int _creaturePerGeneration;
    [SerializeField] private float _timePerRound;
    [SerializeField] private GameObject _prefabsCreature;
    [SerializeField] private Transform _parentCreature;
    private List<Creature> _creatures;

    private void Awake()
    {
        Instance = this;
        Random.InitState(System.DateTime.Now.Second);
    }

    private void Start()
    {
        _creatures = new List<Creature>();
        for (int i = 0; i < _creaturePerGeneration; i++)
        {
            Creature creature = Instantiate(_prefabsCreature, Vector3.zero, Quaternion.identity, _parentCreature).GetComponent<Creature>();
            int rand = Random.Range(0, 10);
            if (rand < 5) creature._nbArticulation = 3;
            else if (rand < 7) creature._nbArticulation = 4;
            else if (rand < 8) creature._nbArticulation = 5;
            else creature._nbArticulation = 6;

            creature.CreatCreature();
            creature.gameObject.SetActive(false);
            _creatures.Add(creature);

        }

        StartCoroutine(BoucleDeGeneration());
    }

    IEnumerator BoucleDeGeneration()
    {
        int numDeGeneration = 0;
        float[] distancePerCreatures = new float[_creaturePerGeneration];
        while (true)
        {
            numDeGeneration++;
            Debug.Log("Génération" + numDeGeneration);
            for (int i = 0; i < _creaturePerGeneration; i++) 
            {
                Debug.Log("Creature " + i);
                
                Vector2 positionStart = _creatures[i].transform.position;
                _creatures[i].gameObject.SetActive(true);

                yield return new WaitForSeconds(_timePerRound);

                Vector2 positionEnd = _creatures[i].transform.position;
                _creatures[i].gameObject.SetActive(false);
                distancePerCreatures[i] = (positionEnd - positionStart).magnitude;

                yield return new WaitForEndOfFrame();

            }


            List<Creature> creatures = new List<Creature>();
            foreach (var indice in HalfBiggestIndices(distancePerCreatures))
            {
                
                creatures.Add(_creatures[indice]);
            }

            yield return new WaitForEndOfFrame();

            for (int i = 0; i < _creatures.Count; i++)
            {
                int rand = Random.Range(0, 3);

                Creature creatureClone = Instantiate(creatures[i], Vector3.zero, Quaternion.identity, _parentCreature).GetComponent<Creature>();
                creatureClone.AddMutation(rand);
                creatureClone.gameObject.SetActive(false);
                _creatures.Add(creatureClone);
            }





            yield return new WaitForEndOfFrame();
        }
    }

    private int[] HalfLowestIndices(float[] arr)
    {
        // Create a copy of the array to keep the original intact
        float[] sortedArr = arr.ToArray();

        // Sort the array in ascending order
        Array.Sort(sortedArr);

        // Determine the number of elements to keep (half of the array)
        int halfLength = arr.Length / 2;

        // Get the indices of the half of the lowest values
        int[] indices = new int[halfLength];
        for (int i = 0; i < halfLength; i++)
        {
            indices[i] = Array.IndexOf(arr, sortedArr[i]);
        }

        return indices;
    }

    private int[] HalfBiggestIndices(float[] arr)
    {
        // Create a copy of the array to keep the original intact
        float[] sortedArr = arr.ToArray();

        // Sort the array in descending order
        Array.Sort(sortedArr, (x, y) => y.CompareTo(x));

        // Determine the number of elements to keep (half of the array)
        int halfLength = arr.Length / 2;

        // Get the indices of the half of the biggest values
        int[] indices = new int[halfLength];
        for (int i = 0; i < halfLength; i++)
        {
            indices[i] = Array.IndexOf(arr, sortedArr[i]);
        }

        return indices;
    }

}
