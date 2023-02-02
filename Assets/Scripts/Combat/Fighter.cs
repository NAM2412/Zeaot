using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System.Collections;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeIntervalBetweenEachAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Transform target;
        float timeSinceLastAttack = 0f;
        CombatTarget combatTarget1;
        private void Start() 
        {
            combatTarget1 = FindObjectOfType<CombatTarget>();
        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveToDestination(target.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
        }

        private  void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeIntervalBetweenEachAttacks)
            {                
                // this will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
                
            }
              
        }

        // Animation event
        private void Hit() 
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamge(weaponDamage);
        }

        private bool GetIsInRange() // is player reach the enemy in weapon's range?
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack (CombatTarget combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this); //  player stops moving and starts to fighting,
            target = combatTarget.transform;    
        }

        public void Cancel()
        {
            Debug.Log("Cancel Moving");
            target = null;
        }
    }
}
