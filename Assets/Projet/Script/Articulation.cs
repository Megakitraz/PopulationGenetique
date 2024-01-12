using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Articulation : MonoBehaviour
{
    public Articulation[] linkArticulations;

    public void CreateArticulationRandomly(Creature creature, int numberOfArticulation)
    {
        int maximum = 3;
        int minimum = 1;
        if (numberOfArticulation < 3) maximum = numberOfArticulation;
        if (numberOfArticulation < minimum) return;

        int otherLinkArticulation = Random.Range(minimum, maximum); 
        int b = Random.Range(0, numberOfArticulation - 1);
        int c = Random.Range(0, numberOfArticulation - b - 1);
        int d = numberOfArticulation - b - c;

        for (int i = 0; i < otherLinkArticulation; i++)
        {
            //Todo Créer des articulations dans un rayon de 1 et faire que les articulations créer des articulations
        }
    }
}
