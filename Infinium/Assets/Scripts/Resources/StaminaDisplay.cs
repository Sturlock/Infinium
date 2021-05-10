using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Infinium.Resources
{
	public class StaminaDisplay : MonoBehaviour
	{
        Stamina stamina;
        private void Awake()
        {
            stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>();
        }

        void Update()
        {
            GetComponent<Image>().fillAmount = stamina.GetPercentage() / 100;
            //print(GetComponent<Image>().fillAmount);

        }
    }
}
