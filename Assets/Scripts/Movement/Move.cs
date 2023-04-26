using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Movement
{
    public class Move : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
    
        NavMeshAgent navMeshAgent;
        Health health;
        void Start() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            
            navMeshAgent.enabled = !health.IsDead; // player can go through enemy's dead body
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
        public void MoveToDestination(Vector3 destination, float speedFraction)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }
        public void StarMoveAction(Vector3 destination, float speedFraction)
        {
            MoveToDestination(destination, speedFraction);
            GetComponent<ActionScheduler>().StartAction(this);
        }

        public object CaptureState()
        {
            return  new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 postion = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false; /* avoid nav mesh interfering, stop the nav mesh agent 
                                                            from meddling wwith postion */
            transform.position = postion.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
