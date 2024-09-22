using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovementBodyMember : MonoBehaviour
{
    [SerializeField] private float m_speed;
    private MovementArticulation[] m_movementArticulations;
    private Vector3 m_dir;

    public void StartMovement(MovementArticulation[] movementArticulations)
    {
        StopAllCoroutines();
        UnityEngine.Random.InitState(System.DateTime.Now.Second);
        m_movementArticulations = movementArticulations;

        StartCoroutine(newVector());
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (enabled)
        {
            if (m_dir != null)
            {
                transform.Translate(m_dir * m_speed * Time.deltaTime);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator newVector()
    {
        while (enabled)
        {

            for (int i = 0; i < m_movementArticulations.Length; i++)
            {
                m_dir = m_movementArticulations[i].strength;

                yield return new WaitForSeconds(m_movementArticulations[i].time);
            }
        }
    }
}