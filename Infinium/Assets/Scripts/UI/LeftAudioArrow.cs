using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftAudioArrow : MonoBehaviour
{
    public Slider sliderValue;
    public void OnClick()
    {
        
        if (sliderValue.value > 0)
        {
            sliderValue.value -= .1f;
            if (sliderValue.value <= 0)
            {
                sliderValue.value = 0;
            }
        }
        //Debug.Log(sliderValue.value);
    }

}
