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
        Health target;
        float timeSinceLastAttack = Mathf.Infinity; 
        CombatTarget combatTarget1;
        private void Start() 
        {
            combatTarget1 = FindObjectOfType<CombatTarget>();
        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead)  return;

            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveToDestination(target.transform.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
        }

        private  void AttackBehaviour()
        {
            //make player look at target first
            transform.LookAt(target.transform);
            // player can attack immediately because timeSinceLastAttack at the very first attack is equal to infinity;
            if (timeSinceLastAttack > timeIntervalBetweenEachAttacks) 
            {
                // this will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");    /*disable trigger "stop attack" to prevent 
                                                                    not-attacking bug from player when player in Attack state*/

            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation event
        private void Hit() 
        {
            if (target == null) { return; }
            target.TakeDamge(weaponDamage);
        }

        private bool GetIsInRange() // is player reach the enemy in weapon's range?
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack (GameObject combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this); //  player stops moving and starts to fighting,
            target = combatTarget.GetComponent<Health>(); 
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) {return false;}

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead;
        }
    }
}
