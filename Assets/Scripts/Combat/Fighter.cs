using UnityEngine;
using RPG.Movement;
using RPG.Core;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        void Update()
        {
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

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
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

        // Animation event
        private void Hit() 
        {
            
        }
    }
}
