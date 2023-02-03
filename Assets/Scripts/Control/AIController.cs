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
        GameObject player;
        Fighter fighter;
        ActionScheduler actionScheduler;
        Health health;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (health.IsDead) return; // if enemy is dead, make it stop chasing player

            if (IsInAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
                
            }
            else
            {
                fighter.Cancel();
            }
        }
        

        private bool IsInAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}
