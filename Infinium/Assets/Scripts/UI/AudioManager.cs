using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string BGSoundeffectPref = "BGSoundeffectPref";
    private static readonly string SoundeffectPref = "SoundeffectPref";

    private int firstPlayInt;
    public Slider backgroundSlider, bgsfxSlider, sfxSlider;
    [SerializeField] private float backroundFloat, bgsfxFloat, sfxFloat;

    //public Sound[] sounds;
    public int scene = 1;

    public AudioSource[] BackgroundAudio;
    public AudioSource[] BGsoundeffectAudio;
    public AudioSource[] SoundeffectAudio;

    public static AudioManager Instance;

    private GameObject[] Go;

    public void Start()
    {
        //scene = SceneManager.GetActiveScene().buildIndex;
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        Go = GameObject.FindGameObjectsWithTag("AudioSettings");
        backgroundSlider = Go[0].GetComponent<Slider>();
        bgsfxSlider = Go[1].GetComponent<Slider>();
        sfxSlider = Go[2].GetComponent<Slider>();
        if (firstPlayInt == 0)
        {
            backroundFloat = .5f;
            bgsfxFloat = .8f;
            sfxFloat = 1f;
            backgroundSlider.value = backroundFloat;
            bgsfxSlider.value = bgsfxFloat;
            sfxSlider.value = sfxFloat;

            PlayerPrefs.SetFloat(BackgroundPref, backroundFloat);
            PlayerPrefs.SetFloat(BGSoundeffectPref, bgsfxFloat);
            PlayerPrefs.SetFloat(SoundeffectPref, sfxFloat);

            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
           backroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backroundFloat;
            bgsfxFloat = PlayerPrefs.GetFloat(BGSoundeffectPref);
            bgsfxSlider.value = bgsfxFloat;
            sfxFloat = PlayerPrefs.GetFloat(SoundeffectPref);
            sfxSlider.value = sfxFloat;
        }
        //for (int i = 0; i < BackgroundAudio.Length; i++)
        //{
            //Debug.Log(i);
        //    if (scene == i)
        //    {
        //        BackgroundAudio[i].Play();
        //    }
        //    else
        //    {
        //        BackgroundAudio[i].Stop();
        //    }
        //}
        //for (int i = 0; i < BGsoundeffectAudio.Length; i++)
        //{
        //    if (scene == i)
        //    {
        //        BGsoundeffectAudio[i].Play();
        //    }
        //    else
        //    {
        //        BGsoundeffectAudio[i].Stop();
        //    }
        //}
    }

    public void Update()
    {
        if (scene != SceneManager.GetActiveScene().buildIndex)
        {
            Debug.Log("Updating...");
            scene = SceneManager.GetActiveScene().buildIndex;
            if (GameObject.FindGameObjectWithTag("Settings") != null)
            {
                Go = GameObject.FindGameObjectsWithTag("AudioSettings");
                backgroundSlider = Go[0].GetComponent<Slider>();
                bgsfxSlider = Go[1].GetComponent<Slider>();
                sfxSlider = Go[2].GetComponent<Slider>();
            }

            for (int i = 0; i < BackgroundAudio.Length; i++)
            {
                //Debug.Log(i);
                if (scene == i)
                {
                    BackgroundAudio[i].Play();
                }
                else
                {
                    BackgroundAudio[i].Stop();
                }
            }

            for (int i = 0; i < BGsoundeffectAudio.Length; i++)
            {
                if (scene == i)
                {
                    BGsoundeffectAudio[i].Play();
                }
                else
                {
                    BGsoundeffectAudio[i].Stop();
                }
            }
        }
    }


    public void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SaveSoundSettings()
    {
       PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
       PlayerPrefs.SetFloat(BGSoundeffectPref, bgsfxSlider.value);
       PlayerPrefs.SetFloat(SoundeffectPref, sfxSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        for (int i = 0; i < BackgroundAudio.Length; i++)
        {
            BackgroundAudio[i].volume = backgroundSlider.value;
        }
        for (int i = 0; i < BGsoundeffectAudio.Length; i++)
        {
            BGsoundeffectAudio[i].volume = bgsfxSlider.value;
        }
        for(int i = 0; i < SoundeffectAudio.Length; i++)
        {
            SoundeffectAudio[i].volume = sfxSlider.value;
        }
    }
}
