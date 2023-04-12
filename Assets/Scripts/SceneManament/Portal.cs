using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SCeneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            


            Portal otherPortal = GetOtherPortal();
            UpdatePlayerSpawnPosition(otherPortal); // player will be spawn at the spawnpoint, not default location in scene


            Destroy(gameObject);
        }

        private void UpdatePlayerSpawnPosition(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal() 
        {
            foreach (Portal portal in  FindObjectsOfType<Portal>())
            {
                if (portal == this) continue; // skip current portal at scene
                return portal;
            }
            return null;
        }
    }
}
