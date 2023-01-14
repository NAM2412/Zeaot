using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target; 
    NavMeshAgent navMeshAgent;
    void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }
}
