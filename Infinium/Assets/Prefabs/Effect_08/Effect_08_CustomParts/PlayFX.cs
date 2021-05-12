using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFX : MonoBehaviour
{
    public ParticleSystem fx;

    // Start is called before the first frame update
    public void Start()
    {
        fx = GetComponentInChildren<ParticleSystem>();
    }

   
    public void PlayEffect()
    {
        fx.Play();
    }

    public void StopEffect()
    {
        fx.Stop();
    }
}
