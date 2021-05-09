using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightAudioArrow : MonoBehaviour
{
    public Slider sliderValue;
    public void OnClick()
    {

        if (sliderValue.value < 1)
        {
            sliderValue.value += .1f;
            if (sliderValue.value >= 1)
            {
                sliderValue.value = 1;
            }
        }
        //Debug.Log(sliderValue.value);
    }
}
