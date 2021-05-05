using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Infinium.SceneManagement
{
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
	    #region Getters
	    public Animator GetAnimator()
	    {
	        return fade;
	    }
	    public float GetFadeTime()
	    {
	        return fadeTime;
	    }
	    public AudioMixer GetAudioMixer()
	    {
	        return AM;
	    }
	
	    #endregion
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
	        
	        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
	        fade.SetTrigger("Start");
	        StartCoroutine(FadeMixerGroup.StartFade(AM,"MainMaster", fadeTime, 0f));
	        
	        //savingWrapper.Save();
	        
	        yield return new WaitForSeconds(fadeTime + 1f);
	        
	        yield return SceneManager.LoadSceneAsync(sceneIndex);
	        savingWrapper.Load();
	    }
	
	    public void ContinueGame()
	    {
	        StartCoroutine(FindObjectOfType<SavingWrapper>().LoadLastSave());
	    }
	
	}
}
