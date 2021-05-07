using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{
    public Button titleButton;
    public Button[] buttons;

    public AudioClip titleAudio;
    public AudioClip buttonAudio;

    public float volume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource titleSource;
        titleSource = titleButton.gameObject.AddComponent<AudioSource>();
        titleSource.clip = titleAudio;
        titleSource.volume = volume;

        titleButton.onClick.AddListener(() => titleSource.Play());
       
        foreach (Button bt in buttons)
        {
            AudioSource audioSource;
            audioSource = bt.gameObject.AddComponent<AudioSource>();
            audioSource.clip = buttonAudio;
            audioSource.volume = volume;

            bt.onClick.AddListener(() => audioSource.Play());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
