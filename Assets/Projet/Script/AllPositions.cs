using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPositions : MonoBehaviour
{
    public float[] allAngle;

    public float[] allTime;

    public float timeCycle;

    private void Start()
    {
        int nbAngle = Random.Range(2, 5);
        allAngle = new float[nbAngle];
        allTime = new float[nbAngle];
        timeCycle = 0;

        for (int i = 0; i < nbAngle; i++)
        {
            allAngle[i] = Random.Range(-180f, 180f);
            allTime[i] = Random.Range(0.2f, 2f);
            timeCycle += allTime[i];
            
        }
    }
}
