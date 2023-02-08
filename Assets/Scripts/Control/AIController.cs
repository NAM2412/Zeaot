using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;
        GameObject player;
        Fighter fighter;
        ActionScheduler actionScheduler;
        Health health;
        Vector3 guardPosition;
        Move mover;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
            mover = GetComponent<Move>();
            guardPosition = transform.position;
        }
        void Update()
        {
            if (health.IsDead) return; // if enemy is dead, make it stop chasing player

            if (IsInAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0; // enemy saw player
                AttackBehaviour();

            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                //Suspicious State
                SuspicionBehaviour(); /*make enemy doesn't run anymore, just stand at their own position*/

            }
            else
            {
                GuardingBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardingBehaviour()
        {
            mover.StarMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool IsInAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
