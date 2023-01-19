using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
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
                if (target == null) continue;
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
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
                    GetComponent<Move>().MoveToDestination(hitDetails.point);
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
