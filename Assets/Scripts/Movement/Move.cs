using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Move : MonoBehaviour, IAction
    {
    
        [SerializeField] Transform target; 
        NavMeshAgent navMeshAgent;
        void Start() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
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

        public void Cancel() 
        {
            
            navMeshAgent.isStopped = true;
        }
        public void MoveToDestination(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.isStopped = false;
        }
        public void StarMoveAction(Vector3 destination)
        {
            MoveToDestination(destination);
            GetComponent<ActionScheduler>().StartAction(this);
        }


    }
}
