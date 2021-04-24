using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] KeyCode SaveButton;
        [SerializeField] KeyCode LoadButton;
        const string defaultSaveFile = "Save";

        void Update()
        {
            if (Input.GetKeyDown(SaveButton))
            {
                GetComponent<SavingSystem>().Save(defaultSaveFile);
            }
            if (Input.GetKeyDown(LoadButton))
            {
                GetComponent<SavingSystem>().Load(defaultSaveFile);
            }
           
        }
    }
}

