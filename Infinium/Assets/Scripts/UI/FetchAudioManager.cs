using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<AudioManager>().Start();
        //FindObjectOfType<AudioManager>().UpdateSound();
    }

    // Update is called once per frame
    public void Updating()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().UpdateSound();
        }
    }



}
