using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update() 
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
    }
    
    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitDetails;
        bool hasHit = Physics.Raycast(ray, out hitDetails);
        if (hasHit)
        {
            GetComponent<Move>().MoveToDestination(hitDetails.point);
        }
    }
}
