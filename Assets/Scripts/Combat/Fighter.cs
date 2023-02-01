using UnityEngine;
using RPG.Movement;
using RPG.Core;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour 
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
                GetComponent<Move>().Stop();
                
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack (CombatTarget combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this); // if player is moving and starting to fighting,
            target = combatTarget.transform;    
        }

        public void CancelAttack()
        {
            target = null;
        }
    }
}
