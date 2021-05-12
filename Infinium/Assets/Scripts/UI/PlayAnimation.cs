using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator anim;
    public bool retract = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void onClick()
    {
        retract = !retract;
        anim.SetBool("retract", retract);
    }

}
