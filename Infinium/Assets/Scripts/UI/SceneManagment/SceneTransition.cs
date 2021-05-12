using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Infinium.SceneManagement
{
	public class SceneTransition : MonoBehaviour
	{
	    public Animator fade;
	    public int scene = 0;
	    public float fadeTime = 1f;
	    public AudioMixer AM;
		SavingWrapper savingWrapper;
		[SerializeField] Button save, load, newGame, continueGame, exit;

		void Start()
	    {
	        StartCoroutine(FadeMixerGroup.StartFade(AM, "MainMaster", fadeTime, 100f));
	        scene = SceneManager.GetActiveScene().buildIndex;
			savingWrapper = FindObjectOfType<SavingWrapper>();
			if (scene == 1)
			{
                save = GameObject.FindGameObjectWithTag("Save").GetComponent<Button>();
                load = GameObject.FindGameObjectWithTag("Load").GetComponent<Button>();

                save.onClick.AddListener(() => savingWrapper.Save());
                load.onClick.AddListener(() => savingWrapper.Load());
            }
            
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

        void Update()
        {
			
        }
        public void onClick()
	    {
	        Transition();
	    }
	
	    public void Transition()
	    {
	        if (scene == 0)
	        {
                try
                {
                    newGame.interactable = false;
                }
                catch (System.Exception ex)
                {
                }

                scene = 1;
	            StartCoroutine(LoadLevel(scene));
	        }
	        else
	        {
                try
                {
                    exit.interactable = false;
                }
                catch (System.Exception ex)
                {
                }
                scene = 0;
	            StartCoroutine(LoadLevel(scene));
	        }
	    }
	
	    IEnumerator LoadLevel(int sceneIndex)
	    {
	        fade.SetTrigger("Start");
	        StartCoroutine(FadeMixerGroup.StartFade(AM,"MainMaster", fadeTime, 0f));
	        
	        //savingWrapper.Save();
	        
	        yield return new WaitForSeconds(fadeTime + 1f);
	        
	        yield return SceneManager.LoadSceneAsync(sceneIndex);
	        //savingWrapper.Load();
	    }
	
	    public void ContinueGame()
	    {
	        StartCoroutine(FindObjectOfType<SavingWrapper>().LoadLastSave());
	    }
	
	}
}
