using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Infinium.Stats
{
	public class ExperienceDisplay : MonoBehaviour
	{
        Experience experience;
        private void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        }

        void Update()
        {
            GetComponent<Image>().fillAmount = experience.GetPercentage();
            print(GetComponent<Image>().fillAmount);

        }
    }
}
