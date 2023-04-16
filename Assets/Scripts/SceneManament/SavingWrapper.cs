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

        private void SaveGame()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        private void LoadGame()
        {

            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
