using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTriggered;
        void Start()
        {
            isTriggered = false;
        }
        void OnTriggerEnter(Collider other)
        {
            
            if (!isTriggered && other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
        }
    }
}
