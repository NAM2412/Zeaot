using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SCeneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 0.2f;

       IEnumerator Start()
       {
            Fader fader = FindObjectOfType<Fader>();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
       }
        void Update()  // Load and do saving API
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGame();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveGame();
            }
        }

        public void SaveGame()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void LoadGame()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);

        }
    }
}
