using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoint = 100f;
        private bool isDead =false;

        public bool IsDead { get => isDead; set => isDead = value; }

        public void TakeDamge (float damage) 
        {
            healthPoint = Mathf.Max(healthPoint - damage,0);
            if (healthPoint == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (IsDead) return;

            // prevent triggering Death'state continuously
            IsDead = true; 
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
