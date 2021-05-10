using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Infinium.Resources
{
	public class HealthDisplay : MonoBehaviour
	{
        Health health;
        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        void Update()
        {
            GetComponent<Image>().fillAmount = health.GetPercentage() / 100;
            //print(GetComponent<Image>().fillAmount);

        }
    }
}
