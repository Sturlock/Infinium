using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Infinium.Saving;

public class SceneTransition : MonoBehaviour
{
    public Animator fade;
    public int scene = 0;
    public float fadeTime = 1f;
    public AudioMixer AM;

    void Start()
    {
        StartCoroutine(FadeMixerGroup.StartFade(AM, "MainMaster", fadeTime, 100f));
        scene = SceneManager.GetActiveScene().buildIndex;
    }

    public void onClick()
    {
        Transition();
    }

    public void Transition()
    {
        if (scene == 0)
        {
            scene = 1;
            StartCoroutine(LoadLevel(scene));
        }
        else
        {
            scene = 0;
            StartCoroutine(LoadLevel(scene));
        }
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        fade.SetTrigger("Start");
        StartCoroutine(FadeMixerGroup.StartFade(AM,"MainMaster", fadeTime, 0f));
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        yield return new WaitForSeconds(fadeTime + 1f);
        SceneManager.LoadSceneAsync(sceneIndex);

        savingWrapper.Load();
        
    }

}
