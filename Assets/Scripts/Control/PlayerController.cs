using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead) return; // if player is dead, disable to controll player

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        #region Interact
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit subHit in hits) 
            {
                
                CombatTarget target = subHit.transform.GetComponent<CombatTarget>();
                if (target == null) { continue; }

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) 
                {
                    continue;
                }
                
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true; // advoid interact with movement when the mouse not down this frame or hovering that target
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hitDetails;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitDetails);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Move>().StarMoveAction(hitDetails.point);
                }
                return true;
            }
            return false;
        }
        #endregion

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
