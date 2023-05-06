using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using System.Collections;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeIntervalBetweenEachAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Health target;
        float maxSpeedValue = 1f;
        float timeSinceLastAttack = Mathf.Infinity; 
        CombatTarget combatTarget1;
        Weapon currentWeapon = null;
        private void Start() 
        {
            combatTarget1 = FindObjectOfType<CombatTarget>();
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }

        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead)  return;

            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveToDestination(target.transform.position, maxSpeedValue);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
        }
        #region  Animation event
        private void Hit() 
        {
            if (target == null) { return; }
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamge(currentWeapon.WeaponDamage);
            }
        }

        private void Shoot () 
        {
            Hit();
        }
        
        #endregion

        #region Attack behaviour
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

        private bool GetIsInRange() // is player reach the enemy in weapon's range?
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.WeaponRange;
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
            GetComponent<Move>().Cancel();
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
        #endregion

        public void EquipWeapon(Weapon weapon) 
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
