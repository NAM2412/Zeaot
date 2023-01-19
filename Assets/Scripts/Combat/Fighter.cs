using UnityEngine;
using RPG.Movement;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour 
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        void Update() 
        {
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            if (target != null && !isInRange)
            {
                GetComponent<Move>().MoveToDestination(target.position);
            }
            else
            {
                GetComponent<Move>().Stop();
            }
        }
        public void Attack (CombatTarget combatTarget) 
        {
            target = combatTarget.transform;    
        }
    }
}