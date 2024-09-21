using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeABody : MonoBehaviour
{
    [SerializeField] private LayerMask m_groundLayer;
    [SerializeField] private GameObject m_prefabsRagdoll;


    void Update()
    {
        //Debug.Log($"Input Mouse Position: {Input.mousePosition}");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_groundLayer) && Input.GetMouseButtonDown(0))
        {
            Instantiate(m_prefabsRagdoll, hit.point + new Vector3(0,5,0), Quaternion.Euler(0, 0, 0));

            Debug.Log("Clique");
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        }
    }
}
