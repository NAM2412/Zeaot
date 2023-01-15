using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
  
    [SerializeField] Transform target; 
    NavMeshAgent navMeshAgent;
    void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        
        
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitDetails;
        bool hasHit = Physics.Raycast(ray, out hitDetails);
        if (hasHit)
        {
            navMeshAgent.SetDestination(hitDetails.point);
        }
    }
}
