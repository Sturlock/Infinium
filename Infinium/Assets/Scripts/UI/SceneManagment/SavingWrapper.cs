using Infinium.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Infinium.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] KeyCode SaveButton;
        [SerializeField] KeyCode LoadButton;
        const string defaultSaveFile = "saveGame";

        [SerializeField] float fadeInTime = 0.2f;

        public IEnumerator LoadLastSave()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);

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

