using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] KeyCode SaveButton;
        [SerializeField] KeyCode LoadButton;
        const string defaultSaveFile = "saveGame";

        void Start()
        {
            
        }

        void Update()
        {
            if (Input.GetKeyDown(SaveButton))
            {
                Save();
            }
            if (Input.GetKeyDown(LoadButton))
            {
                Load();
            }

        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}

