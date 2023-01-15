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
        UpdateAnimator();
        
    }

    private void UpdateAnimator()
    {
        Vector3 globalVelocity = navMeshAgent.velocity;
        // taking x,y,z from global and converting into local
        Vector3 localVelocity = transform.InverseTransformDirection(globalVelocity);
        /*Why have to do that?*/
        /*when we're creating our velocity, nav mesh velocity that stored as the globalVelocity variable
        within this update animated method, we are grabbing the global coordinates.
        So the global velocity and it might be X is 273 and Y is 57 and the velocity is changing at a particular
        rate that shows us where we are within the world, within world space.
        And that's not so useful for our animator.
        Our animator just wants to know Is Character running forward or is not running forward?
        */
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed",speed);
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
