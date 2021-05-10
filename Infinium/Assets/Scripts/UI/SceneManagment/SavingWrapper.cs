using Infinium.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Infinium.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        Button save, load;
        [SerializeField] KeyCode saveButton, loadButton;
        const string defaultSaveFile = "saveGame";

        [SerializeField] float fadeInTime = 0.2f;

        void Awake()
        {
            save = GameObject.FindGameObjectWithTag("Save").GetComponent<Button>();
            load = GameObject.FindGameObjectWithTag("Load").GetComponent<Button>();

        }

        public IEnumerator LoadLastSave()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);

        }

        void Update()
        {

            save.onClick.AddListener(() => Save());
            load.onClick.AddListener(() => Load());

            if (Input.GetKeyDown(saveButton))
            {
                Save();
            }
            if (Input.GetKeyDown(loadButton))
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

