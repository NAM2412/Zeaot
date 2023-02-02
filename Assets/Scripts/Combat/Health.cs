using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoint = 100f;
        private bool isDead =false;

        public void TakeDamge (float damage) 
        {
            healthPoint = Mathf.Max(healthPoint - damage,0);
            Debug.Log(healthPoint);
            if (healthPoint == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            // prevent triggering Death'state continuously
            isDead = true; 
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
